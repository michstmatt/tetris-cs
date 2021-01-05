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
        private BlockQueue queue;
        private AbstractBlock currBlock; 
        private AbstractBlock holdBlock;
        private HoldBlock holdBlockDisplay;
        private Queue<AbstractBlock> nextBlocks;
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
            queue = new BlockQueue();
            holdBlockDisplay = new HoldBlock();

            queue.Position = position + new Vector2(board.Size.X /2  + 20, 0);
            holdBlockDisplay.Position = position - new Vector2(board.Size.X , 0);

            nextBlocks = new Queue<AbstractBlock>();
            board.Position = position - new Vector2(board.Size.X / 2, 0);

            for (int i = 0; i < 5; i++)
            {
                AddBlock();
            }

            currBlock = nextBlocks.Dequeue();
            board.UpdateBlock(currBlock);
            _lastUpdate = DateTime.Now.ToFileTime();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            Tetris.Graphics.Textures.Init(GraphicsDevice);

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

            nextBlocks.Enqueue(b);
        }

        public void HoldBlock()
        {
            var tmp = holdBlock;
            if(holdBlock == null)
            {
                tmp = nextBlocks.Dequeue();
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
                if (delta > 1000)
                {
                    _lastUpdate = time;
                    if (board.BlockCanMove(currBlock, 0, 1)) currBlock.MoveDown(1);
                }
                currBlock.Update(delta);
                board.UpdateBlock(currBlock);

                if (currBlock.IsSet)
                {
                    currBlock = nextBlocks.Dequeue();
                    AddBlock();
                    board.Update();
                }
            }
            queue.Blocks = nextBlocks.ToList();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            board.DrawCells(_spriteBatch, 20);
            queue.DrawCells(_spriteBatch, 20);
            holdBlockDisplay.DrawCells(_spriteBatch, 20);
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
