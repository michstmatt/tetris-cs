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
        private int score = 0;
        private float speed = 1.0f;
        private TimeSpan totalTime;
        private float blockUpdateDelta = 1000;
    
        private bool gameLost = false;

        private Scoreboard scoreboard;

        private AbstractBlock currBlock; 
        private AbstractBlock holdBlock;
        private BlockDisplay holdBlockDisplay;
        private BlockDisplay nextBlockDisplay;
        private Queue<AbstractBlock> blockQueue;
        private InputManager inputManager;
        private bool paused;

        private TimeSpan _lastUpdate;

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
            nextBlockDisplay = new BlockDisplay();
            holdBlockDisplay = new BlockDisplay();
            scoreboard = new Scoreboard();

            nextBlockDisplay.Position = position + new Vector2(board.Size.X /2  + 20, 0);
            holdBlockDisplay.Position = position - new Vector2(board.Size.X , 0);


            scoreboard.Position = position + new Vector2(board.Size.X /2  + 20, nextBlockDisplay.Size.Y + 200);

            blockQueue = new Queue<AbstractBlock>();
            board.Position = position - new Vector2(board.Size.X / 2, 0);

            for (int i = 0; i < 5; i++)
            {
                AddBlock();
            }

            currBlock = blockQueue.Dequeue();
            board.UpdateBlock(currBlock);
            _lastUpdate = TimeSpan.FromSeconds(0);
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

            if (!paused && !gameLost)
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
                var time = gameTime.TotalGameTime; 
                var delta = time - _lastUpdate;
                totalTime += gameTime.ElapsedGameTime;


                if (delta.TotalMilliseconds > blockUpdateDelta)
                {
                    _lastUpdate = time;
                    if (board.BlockCanMove(currBlock, 0, 1)) currBlock.MoveDown(1);
                }

                currBlock.Update((float)gameTime.ElapsedGameTime.TotalMilliseconds);
                board.UpdateBlock(currBlock);

                if (currBlock.IsSet)
                {
                    currBlock = blockQueue.Dequeue();
                    AddBlock();
                    score += board.Update();
                    gameLost = board.DidLose();
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
            scoreboard.Draw(_spriteBatch, (int)totalTime.TotalSeconds, score);

            if (paused)
            {
                _spriteBatch.DrawString(Tetris.Graphics.Textures.Font, "Paused", new Vector2(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height /2), Color.White);
            }
            else if (gameLost)
            {
                _spriteBatch.DrawString(Tetris.Graphics.Textures.Font, "You Lost", new Vector2(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height /2), Color.Red);
            }
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
