// здесь должна быть форма с элементами (windows forms), скину скрином
using System;
using System.Windows.Forms;

namespace LibraryApp;

public partial class EditForm : Form
    {
        private readonly Book _book;

        public EditForm(Book? book = null)
        {
            InitializeComponent();

            // Без логики, ломающей дизайнер
            _book = book != null
                ? new Book
                {
                    Id = book.Id,
                    Title = book.Title,
                    Author = book.Author,
                    Year = book.Year,
                    Price = book.Price,
                    IsAvailable = book.IsAvailable,
                    Genre = book.Genre
                }
                : new Book();

            Text = book == null ? "Добавление книги" : "Редактирование книги";

            // События (если не настроишь в дизайнере)
            Load += EditForm_Load;
            btnSave.Click += btnSave_Click; // TODO: btnSave — имя кнопки "Сохранить"
        }

        public Book ResultBook => _book;

        private void EditForm_Load(object? sender, EventArgs e)
        {
            // Привязка данных
            cmbGenre.DataSource = Enum.GetValues(typeof(Genre)); // TODO: cmbGenre
            txtTitle.DataBindings.Add("Text", _book, nameof(Book.Title), false, DataSourceUpdateMode.OnPropertyChanged);
            txtAuthor.DataBindings.Add("Text", _book, nameof(Book.Author), false, DataSourceUpdateMode.OnPropertyChanged);
            numYear.DataBindings.Add("Value", _book, nameof(Book.Year), false, DataSourceUpdateMode.OnPropertyChanged);
            chkIsAvailable.DataBindings.Add("Checked", _book, nameof(Book.IsAvailable), false, DataSourceUpdateMode.OnPropertyChanged);
            cmbGenre.DataBindings.Add("SelectedItem", _book, nameof(Book.Genre), false, DataSourceUpdateMode.OnPropertyChanged);

            txtPrice.Text = _book.Price.ToString(); // TODO: txtPrice
        }

        private void btnSave_Click(object? sender, EventArgs e)
        {
            errorProvider1.Clear(); // TODO: errorProvider1
            bool ok = true;

            if (string.IsNullOrWhiteSpace(txtTitle.Text))
            {
                errorProvider1.SetError(txtTitle, "Название не может быть пустым");
                ok = false;
            }
            if (string.IsNullOrWhiteSpace(txtAuthor.Text))
            {
                errorProvider1.SetError(txtAuthor, "Автор не может быть пустым");
                ok = false;
            }
            if (!decimal.TryParse(txtPrice.Text, out var price) || price < 0)
            {
                errorProvider1.SetError(txtPrice, "Некорректная цена");
                ok = false;
            }
            else
            {
                _book.Price = price;
            }

            if (!ok)
            {
                DialogResult = DialogResult.None; // отменяем закрытие
            }
        }
    }
