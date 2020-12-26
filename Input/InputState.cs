namespace Tetris.Input
{
    public class InputState
    {
        public bool isLeftPressed {get; set;} = false;
        public bool isRightPressed {get; set;} = false;
        public bool isUpPressed {get; set;} = false;
        public bool isDownPressed {get; set;} = false;
        public bool isRotateRightPressed {get; set;} = false;
        public bool isRotateLeftPressed {get; set;} = false;
        public bool isPausePressed {get; set;} = false;
        public bool isHoldPressed {get; set;} = false;
    }
}