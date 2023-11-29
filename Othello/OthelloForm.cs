namespace Othello
{
    public partial class OthelloForm : Form
    {
        private const int _boardSize = 8;
        private Button[,] _btns;
        private bool _side;

        public OthelloForm()
        {
            InitializeComponent();
            InitializeBoard();
        }

        private void InitializeBoard()
        {
            const int btnSize = 30;
            const int gap = 2;
            _btns = new Button[_boardSize, _boardSize]!;

            for (var i = 0; i < _boardSize; i++)
            {
                for (var j = 0; j < _boardSize; j++)
                {
                    var button = new Button
                    {
                        Width = btnSize,
                        Height = btnSize,
                        Location = new Point(j * (btnSize + gap) + gap, i * (btnSize + gap) + gap),
                        Tag = new StoneInfo(new Point(i, j)),
                        TextAlign = ContentAlignment.MiddleCenter
                    };

                    button.Click += BoardButtonClick!;
                    _btns[i, j] = button;
                    Controls.Add(button);
                }
            }
        }

        private void BoardButtonClick(object sender, EventArgs e)
        {
            var button = (Button)sender;

            if(button.Tag is  StoneInfo stoneInfo)
            {
                if(stoneInfo.Side != null)
                {
                    return;
                }

                stoneInfo.Side = _side;
                button.BackColor = _side ? Color.Black : Color.White;
                _side = !_side;
            }
        }
    }
}