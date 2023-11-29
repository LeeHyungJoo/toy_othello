using System.ComponentModel;

namespace Othello
{
    public partial class OthelloForm : Form
    {
        private const int _boardSize = 8;
        private Button[,] _btns;
        private bool _side;
        private readonly int[] dy = { +1, +1, +0, -1, -1, -1, +0, +1 };
        private readonly int[] dx = { +0, +1, +1, +1, +0, -1, -1, -1 };
        private bool _gameover;
        private int _whiteCount;
        private int _blackCount;


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

                    if ((i == 3 && j == 3) || (i == 4 && j == 4))
                    {
                        if (button.Tag is StoneInfo info)
                            info.Side = false;
                        button.BackColor = Color.White;
                    }

                    if ((i == 3 && j == 4) || (i == 4 && j == 3))
                    {
                        if (button.Tag is StoneInfo info)
                            info.Side = true;
                        button.BackColor = Color.Black;
                    }

                    _btns[i, j] = button;
                    Controls.Add(button);
                }
            }

            _blackCount = _whiteCount = 2;
            lb_blackScore.Text = _blackCount.ToString();
            lb_whiteScore.Text = _whiteCount.ToString();
        }

        private void BoardButtonClick(object sender, EventArgs e)
        {
            if (_gameover)
                return;

            var button = (Button)sender;

            if (button.Tag is StoneInfo stoneInfo)
            {
                if (stoneInfo.Side != null)
                    return;

                //8방향 모두 체크
                var pivot = stoneInfo.Coord;
                Dictionary<int, Point?> nearestPointByDirection = new Dictionary<int, Point?>();
                for (int dir = 0; dir < 8; dir++)
                {
                    nearestPointByDirection.Add(dir, null);
                    for (int dist = 1; dist < _boardSize; dist++)
                    {
                        var nextPoint = Point.Add(pivot, new Size(dist * dx[dir], dist * dy[dir]));

                        if (nextPoint.X < 0 || nextPoint.Y < 0 || nextPoint.X > _boardSize - 1 || nextPoint.Y > _boardSize - 1)
                            continue;

                        if (_btns[nextPoint.X, nextPoint.Y].Tag is StoneInfo nextStoneInfo)
                        {
                            if (nextStoneInfo.Side == null)
                                break;

                            if (nextStoneInfo.Side == _side)
                            {
                                nearestPointByDirection[dir] = nextPoint;
                                break;
                            }
                            else
                                continue;
                        }
                    }
                }

                bool isValid = false;
                int count = 0;
                for (int dir = 0; dir < 8; dir++)
                {
                    if (!nearestPointByDirection.TryGetValue(dir, out var startPoint) || startPoint == null)
                        continue;

                    bool check = false;
                    List<Button> checkBtns = new List<Button>();
                    for (int dist = _boardSize - 1; dist >= 1; dist--)
                    {
                        var nextPoint = Point.Add(pivot, new Size(dist * dx[dir], dist * dy[dir]));

                        if (nextPoint.X < 0 || nextPoint.Y < 0 || nextPoint.X > _boardSize - 1 || nextPoint.Y > _boardSize - 1)
                            continue;

                        if (startPoint.Value.X == nextPoint.X && startPoint.Value.Y == nextPoint.Y)
                        {
                            check = true;
                            continue;
                        }

                        if (check) checkBtns.Add(_btns[nextPoint.X, nextPoint.Y]);
                    }

                    foreach (var checkBtn in checkBtns)
                    {
                        isValid = true;

                        if (checkBtn.Tag is StoneInfo nextStoneInfo)
                            nextStoneInfo.Side = _side;
                        checkBtn.BackColor = _side ? Color.Black : Color.White;
                    }

                    count += checkBtns.Count;
                }

                if (isValid)
                {
                    stoneInfo.Side = _side;
                    button.BackColor = _side ? Color.Black : Color.White;

                    if (_side)
                    {
                        _blackCount = 1 + _blackCount + count;
                        _whiteCount -= count;
                    }
                    else
                    {
                        _whiteCount = 1 + _whiteCount + count;
                        _blackCount -= count;
                    }

                    lb_blackScore.Text = _blackCount.ToString();
                    lb_whiteScore.Text = _whiteCount.ToString();

                    _side = !_side;

                    if (_blackCount == 0 || _whiteCount == 0 || _whiteCount + _blackCount == _boardSize * _boardSize)
                    {
                        _gameover = true;
                        ShowGameOverMessageBox();
                    }
                }
            }
        }

        private void ShowGameOverMessageBox()
        {
            string target;
            if (_blackCount > _whiteCount)
                target = "BLACK";
            else if (_blackCount < _whiteCount)
                target = "WHITE";
            else
                target = "both";

            var message = $"Game Over! {target} wins!";
            var result = MessageBox.Show(message, "Game Over", MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                _gameover = false;
                InitializeComponent();
                InitializeBoard();
            }
            else if (result == DialogResult.No)
            {
                Application.Exit();
            }
        }
    }
}