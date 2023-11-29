namespace Othello
{
    public partial class OthelloForm : Form
    {
        private const int BoardSize = 8;
        private Button[,] boardButtons;

        public OthelloForm()
        {
            InitializeComponent();
            InitializeBoard();
        }

        private void InitializeBoard()
        {
            const int ButtonSize = 30;
            const int Gap = 2;
            boardButtons = new Button[BoardSize, BoardSize]!;

            for (var i = 0; i < BoardSize; i++)
            {
                for (var j = 0; j < BoardSize; j++)
                {
                    var button = new Button
                    {
                        Width = ButtonSize,
                        Height = ButtonSize,
                        Location = new Point(j * (ButtonSize + Gap) + Gap, i * (ButtonSize + Gap) + Gap),
                        Tag = new Point(i, j),
                        Font = new Font("Arial", 12),
                        Text = "",
                        TextAlign = ContentAlignment.MiddleCenter
                    };

                    button.Click += BoardButtonClick!;
                    boardButtons[i, j] = button;
                    Controls.Add(button);
                }
            }
        }

        private void BoardButtonClick(object sender, EventArgs e)
        {
            var button = (Button)sender;
            var point = (Point)button.Tag!;
        }
    }
}