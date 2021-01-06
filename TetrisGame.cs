using System;
using System.Linq;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using Tetris.Models;
using Tetris.Input;

namespace Tetris
{
    public class TetrisGame : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private Board board;
        //private BlockQueue queue;
        private AbstractBlock currBlock; 
        private AbstractBlock holdBlock;
        private HoldBlock holdBlockDisplay;
        private HoldBlock nextBlockDisplay;
        private Queue<AbstractBlock> blockQueue;
        private InputManager inputManager;
        private bool paused;

        private long _lastUpdate;

        public TetrisGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            inputManager = new InputManager();
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
            paused = false;
            var position = new Vector2(GraphicsDevice.Viewport.Width / 2, 20);
            board = new Board();
            nextBlockDisplay = new HoldBlock();
            holdBlockDisplay = new HoldBlock();

            nextBlockDisplay.Position = position + new Vector2(board.Size.X /2  + 20, 0);
            holdBlockDisplay.Position = position - new Vector2(board.Size.X , 0);

            blockQueue = new Queue<AbstractBlock>();
            board.Position = position - new Vector2(board.Size.X / 2, 0);

            for (int i = 0; i < 5; i++)
            {
                AddBlock();
            }

            currBlock = blockQueue.Dequeue();
            board.UpdateBlock(currBlock);
            _lastUpdate = DateTime.Now.ToFileTime();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            var f = Content.Load<SpriteFont>("Font/font");
            Tetris.Graphics.Textures.Init(GraphicsDevice, f);

            // TODO: use this.Content to load your game content here
        }

        private void AddBlock()
        {
            var key = new Random().Next(7);
            AbstractBlock b = new TBlock(0, 5);
            if (key == 1) b = new SBlock(0, 5);
            if (key == 2) b = new ZBlock(0, 5);
            if (key == 3) b = new LBlock(0, 5);
            if (key == 4) b = new JBlock(0, 5);
            if (key == 5) b = new Square(0, 5);
            if (key == 6) b = new Straight(0, 5);

            blockQueue.Enqueue(b);
        }

        public void HoldBlock()
        {
            var tmp = holdBlock;
            if(holdBlock == null)
            {
                tmp = blockQueue.Dequeue();
                AddBlock();
            }
            holdBlock = currBlock;
            holdBlock.X = 5;
            holdBlock.Y = 0;
            currBlock = tmp;
            holdBlockDisplay.Block = holdBlock;
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            var state = inputManager.HandleInput();

            if(state.isPausePressed) paused = !paused;

            if (!paused)
            {
                board.ClearBlock(currBlock);

                if (state.isUpPressed)
                { 
                    while(board.BlockCanMove(currBlock, 0, 1))
                    {
                        currBlock.MoveDown(1);
                    }
                    currBlock.IsSet = true;
                }
                if (state.isDownPressed && board.BlockCanMove(currBlock, 0, 1)) currBlock.MoveDown(1);
                if (state.isRightPressed && board.BlockCanMove(currBlock, 1, 0)) currBlock.HorizontalMove(true);
                if (state.isLeftPressed && board.BlockCanMove(currBlock, -1, 0)) currBlock.HorizontalMove(false);
                if (state.isRotateLeftPressed) currBlock.RotateLeft();
                if (state.isRotateRightPressed) currBlock.RotateRight();
                if (state.isHoldPressed) HoldBlock();


                // TODO: Add your update logic here
                var time = DateTime.Now.ToFileTime();
                var delta = (time - _lastUpdate) / 10000;
                if (delta > 400)
                {
                    _lastUpdate = time;
                    if (board.BlockCanMove(currBlock, 0, 1)) currBlock.MoveDown(1);
                }
                currBlock.Update(delta);
                board.UpdateBlock(currBlock);

                if (currBlock.IsSet)
                {
                    currBlock = blockQueue.Dequeue();
                    AddBlock();
                    board.Update();
                }
            }
            nextBlockDisplay.Block = blockQueue.Peek();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            //int cellSize = Math.Min(GraphicsDevice.Viewport.Width / 20, GraphicsDevice.Viewport.Height /10);
            int cellSize = 20;

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            board.DrawCells(_spriteBatch, cellSize);
            nextBlockDisplay.Draw(_spriteBatch, "Next", cellSize);
            holdBlockDisplay.Draw(_spriteBatch, "Hold", cellSize);
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
