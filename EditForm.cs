using System.Windows.Forms;

namespace LibraryApp;

public class EditForm : Form
{
    private readonly Book _book;

    private TextBox txtTitle = null!;
    private TextBox txtAuthor = null!;
    private NumericUpDown nudYear = null!;
    private TextBox txtPrice = null!;
    private ComboBox cmbGenre = null!;
    private CheckBox chkInStock = null!;
    private Button btnSave = null!;
    private Button btnCancel = null!;
    private ErrorProvider errorProvider = null!;

    public EditForm(Book? book = null)
    {
        _book = book != null
            ? new Book
            {
                Id = book.Id,
                Title = book.Title,
                Author = book.Author,
                Year = book.Year,
                Price = book.Price,
                Genre = book.Genre,
                InStock = book.InStock
            }
            : new Book();

        InitializeUI();
        SetupDataBindings();

        Text = book == null ? "Добавление книги" : "Редактирование книги";
    }

    public Book ResultBook => _book;

    private void InitializeUI()
    {
        // Form settings
        ClientSize = new System.Drawing.Size(350, 300);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        StartPosition = FormStartPosition.CenterParent;
        MaximizeBox = false;
        MinimizeBox = false;

        errorProvider = new ErrorProvider();
        errorProvider.ContainerControl = this;

        int labelX = 20;
        int inputX = 120;
        int inputWidth = 200;
        int rowHeight = 30;
        int currentY = 20;

        // Title
        var lblTitle = new Label { Text = "Название", Location = new System.Drawing.Point(labelX, currentY + 3), AutoSize = true };
        txtTitle = new TextBox { Location = new System.Drawing.Point(inputX, currentY), Width = inputWidth };
        Controls.Add(lblTitle);
        Controls.Add(txtTitle);
        currentY += rowHeight;

        // Author
        var lblAuthor = new Label { Text = "Автор", Location = new System.Drawing.Point(labelX, currentY + 3), AutoSize = true };
        txtAuthor = new TextBox { Location = new System.Drawing.Point(inputX, currentY), Width = inputWidth };
        Controls.Add(lblAuthor);
        Controls.Add(txtAuthor);
        currentY += rowHeight;

        // Year
        var lblYear = new Label { Text = "Год", Location = new System.Drawing.Point(labelX, currentY + 3), AutoSize = true };
        nudYear = new NumericUpDown { Location = new System.Drawing.Point(inputX, currentY), Width = inputWidth, Minimum = 1800, Maximum = 2100 };
        Controls.Add(lblYear);
        Controls.Add(nudYear);
        currentY += rowHeight;

        // Price
        var lblPrice = new Label { Text = "Цена", Location = new System.Drawing.Point(labelX, currentY + 3), AutoSize = true };
        txtPrice = new TextBox { Location = new System.Drawing.Point(inputX, currentY), Width = inputWidth };
        Controls.Add(lblPrice);
        Controls.Add(txtPrice);
        currentY += rowHeight;

        // Genre
        var lblGenre = new Label { Text = "Жанр", Location = new System.Drawing.Point(labelX, currentY + 3), AutoSize = true };
        cmbGenre = new ComboBox { Location = new System.Drawing.Point(inputX, currentY), Width = inputWidth, DropDownStyle = ComboBoxStyle.DropDownList };
        cmbGenre.DataSource = Enum.GetValues(typeof(Genre));
        Controls.Add(lblGenre);
        Controls.Add(cmbGenre);
        currentY += rowHeight;

        // InStock
        chkInStock = new CheckBox { Text = "В наличии", Location = new System.Drawing.Point(inputX, currentY), AutoSize = true };
        Controls.Add(chkInStock);
        currentY += rowHeight + 20;

        // Buttons
        btnSave = new Button { Text = "Сохранить", Location = new System.Drawing.Point(80, currentY), Width = 90, DialogResult = DialogResult.OK };
        btnCancel = new Button { Text = "Отменить", Location = new System.Drawing.Point(180, currentY), Width = 90, DialogResult = DialogResult.Cancel };

        btnSave.Click += BtnSave_Click;

        Controls.Add(btnSave);
        Controls.Add(btnCancel);

        AcceptButton = btnSave;
        CancelButton = btnCancel;
    }

    private void SetupDataBindings()
    {
        txtTitle.DataBindings.Add("Text", _book, nameof(Book.Title), false, DataSourceUpdateMode.OnPropertyChanged);
        txtAuthor.DataBindings.Add("Text", _book, nameof(Book.Author), false, DataSourceUpdateMode.OnPropertyChanged);
        nudYear.DataBindings.Add("Value", _book, nameof(Book.Year), false, DataSourceUpdateMode.OnPropertyChanged);
        chkInStock.DataBindings.Add("Checked", _book, nameof(Book.InStock), false, DataSourceUpdateMode.OnPropertyChanged);
        cmbGenre.SelectedItem = _book.Genre;
        cmbGenre.SelectedIndexChanged += (s, e) => _book.Genre = (Genre)cmbGenre.SelectedItem!;
        txtPrice.Text = _book.Price.ToString();
    }

    private void BtnSave_Click(object? sender, EventArgs e)
    {
        errorProvider.Clear();
        bool isValid = true;

        if (string.IsNullOrWhiteSpace(txtTitle.Text))
        {
            errorProvider.SetError(txtTitle, "Название не может быть пустым");
            isValid = false;
        }

        if (string.IsNullOrWhiteSpace(txtAuthor.Text))
        {
            errorProvider.SetError(txtAuthor, "Автор не может быть пустым");
            isValid = false;
        }

        if (!decimal.TryParse(txtPrice.Text, out var price) || price < 0)
        {
            errorProvider.SetError(txtPrice, "Некорректная цена");
            isValid = false;
        }
        else
        {
            _book.Price = price;
        }

        if (!isValid)
        {
            DialogResult = DialogResult.None;
        }
    }
}
