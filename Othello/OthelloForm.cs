namespace Othello
{
    public partial class OthelloForm : Form
    {
        private const int _boardSize = 8;
        private Button[,] _btns;
        private bool _side;
        private readonly int[] dy = { +1, +1, +0, -1, -1, -1, +0, +1 };
        private readonly int[] dx = { +0, +1, +1, +1, +0, -1, -1, -1 };


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
                        if(button.Tag is StoneInfo info)
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
        }

        private void BoardButtonClick(object sender, EventArgs e)
        {
            var button = (Button)sender;

            if (button.Tag is StoneInfo stoneInfo)
            {
                if (stoneInfo.Side != null)
                    return;

                //8���� ��� üũ
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

                            if(nextStoneInfo.Side == _side)
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
                for(int dir = 0; dir < 8; dir ++)
                {
                    if (!nearestPointByDirection.TryGetValue(dir, out var startPoint) || startPoint == null)
                        continue;

                    bool check = false;
                    List<Button> checkBtns = new List<Button>();
                    for(int dist = _boardSize - 1; dist >= 1; dist--)
                    {
                        var nextPoint = Point.Add(pivot, new Size(dist * dx[dir], dist * dy[dir]));

                        if (nextPoint.X < 0 || nextPoint.Y < 0 || nextPoint.X > _boardSize - 1 || nextPoint.Y > _boardSize - 1)
                            continue;

                        if(startPoint.Value.X == nextPoint.X && startPoint.Value.Y == nextPoint.Y)
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
                }

                if(isValid)
                {
                    stoneInfo.Side = _side;
                    button.BackColor = _side ? Color.Black : Color.White;
                    _side = !_side;
                }
            }
        }
    }
}