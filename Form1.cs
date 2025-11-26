// это форма с элементами (windows forms), скину скрином
using System.ComponentModel;

namespace LibraryApp;

public partial class Form1 : Form
    {
        private BindingList<Book> _books = new();
        private int _loadedCount = 0;
        private const int PageSize = 50;

        public Form1()
        {
            InitializeComponent();
            Load += Form1_Load;
        }

        private void Form1_Load(object? sender, EventArgs e)
        {
            Text = "Библиотека";
            StartPosition = FormStartPosition.CenterScreen;

            // Основные настройки таблицы (если не выставил в дизайнере)
            gridBooks.ReadOnly = true;                               // TODO: gridBooks — имя DataGridView
            gridBooks.AllowUserToAddRows = false;
            gridBooks.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            gridBooks.MultiSelect = false;

            var loaded = FileService.LoadData();
            _books = new BindingList<Book>(loaded);
            gridBooks.DataSource = _books;
            ConfigureGridColumns();

            // Кнопки
            btnAdd.Click += btnAdd_Click;       // TODO: замени имя, если отличается
            btnEdit.Click += btnEdit_Click;     // TODO
            btnDelete.Click += btnDelete_Click; // TODO

            // Меню и контекстное меню — создаём программно, чтобы обойти глюки дизайнера
            BuildMainMenu();
            BuildContextMenu();

            // Правый клик — выделение строки под курсором
            gridBooks.MouseDown += gridBooks_MouseDown;
        }

        private void ConfigureGridColumns()
        {
            if (gridBooks.Columns["Id"] != null) gridBooks.Columns["Id"].Visible = false;
            if (gridBooks.Columns["Title"] != null) gridBooks.Columns["Title"].HeaderText = "Название";
            if (gridBooks.Columns["Author"] != null) gridBooks.Columns["Author"].HeaderText = "Автор";
            if (gridBooks.Columns["Year"] != null) gridBooks.Columns["Year"].HeaderText = "Год";
            if (gridBooks.Columns["Price"] != null) gridBooks.Columns["Price"].HeaderText = "Цена";
            if (gridBooks.Columns["IsAvailable"] != null) gridBooks.Columns["IsAvailable"].HeaderText = "Наличие";
            if (gridBooks.Columns["Genre"] != null) gridBooks.Columns["Genre"].HeaderText = "Жанр";
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            FileService.SaveData(_books.ToList());
            base.OnFormClosing(e);
        }

        // ----- КНОПКИ -----
        private void btnAdd_Click(object? sender, EventArgs e)
        {
            using var editForm = new EditForm();
            if (editForm.ShowDialog() == DialogResult.OK)
            {
                _books.Add(editForm.ResultBook);
                FileService.AppendBook(editForm.ResultBook);
            }
        }

        private void btnEdit_Click(object? sender, EventArgs e)
        {
            if (gridBooks.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите книгу.", "Редактирование");
                return;
            }
            var selected = (Book)gridBooks.SelectedRows[0].DataBoundItem!;
            using var editForm = new EditForm(selected);
            if (editForm.ShowDialog() == DialogResult.OK)
            {
                int idx = _books.IndexOf(selected);
                _books[idx] = editForm.ResultBook;
            }
        }

        private void btnDelete_Click(object? sender, EventArgs e)
        {
            if (gridBooks.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите книгу для удаления.", "Удаление");
                return;
            }
            var book = (Book)gridBooks.SelectedRows[0].DataBoundItem!;
            var confirm = MessageBox.Show($"Удалить \"{book.Title}\"?", "Подтверждение",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm == DialogResult.Yes)
                _books.Remove(book);
        }

        // ----- МЕНЮ (создаём програмно) -----
        private void BuildMainMenu()
        {
            // menuStrip1 — должен существовать на форме (пустой)
            menuStrip1.Items.Clear(); // TODO: menuStrip1 — имя MenuStrip

            var file = new ToolStripMenuItem("Файл");
            var fileLoad = new ToolStripMenuItem("Загрузить", null, fileLoadMenuItem_Click);
            var fileSave = new ToolStripMenuItem("Сохранить", null, fileSaveMenuItem_Click);
            var fileExit = new ToolStripMenuItem("Выход", null, fileExitMenuItem_Click);
            file.DropDownItems.AddRange(new ToolStripItem[] { fileLoad, fileSave, fileExit });

            var edit = new ToolStripMenuItem("Правка");
            var editAdd = new ToolStripMenuItem("Добавить", null, (s, e) => btnAdd_Click(s, e));
            var editEdit = new ToolStripMenuItem("Редактировать", null, (s, e) => btnEdit_Click(s, e));
            var editDelete = new ToolStripMenuItem("Удалить", null, (s, e) => btnDelete_Click(s, e));
            edit.DropDownItems.AddRange(new ToolStripItem[] { editAdd, editEdit, editDelete });

            var help = new ToolStripMenuItem("Справка");
            var helpAbout = new ToolStripMenuItem("О программе", null, helpAboutMenuItem_Click);
            help.DropDownItems.Add(helpAbout);

            menuStrip1.Items.AddRange(new ToolStripItem[] { file, edit, help });
            MainMenuStrip = menuStrip1;
        }

        private void BuildContextMenu()
        {
            var ctx = new ContextMenuStrip();
            ctx.Items.Add("Добавить", null, (s, e) => btnAdd_Click(s, e));
            ctx.Items.Add("Редактировать", null, (s, e) => btnEdit_Click(s, e));
            ctx.Items.Add("Удалить", null, (s, e) => btnDelete_Click(s, e));
            gridBooks.ContextMenuStrip = ctx; // привязываем к таблице
        }

        // Обработчики пунктов меню "Файл"
        private void fileLoadMenuItem_Click(object? sender, EventArgs e)
        {
            var loaded = FileService.LoadData();
            _books = new BindingList<Book>(loaded);
            gridBooks.DataSource = _books;
            ConfigureGridColumns();
        }

        private void fileSaveMenuItem_Click(object? sender, EventArgs e)
        {
            FileService.SaveData(_books.ToList());
            MessageBox.Show("Данные сохранены.", "Сохранение", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void fileExitMenuItem_Click(object? sender, EventArgs e) => Close();

        private void helpAboutMenuItem_Click(object? sender, EventArgs e)
        {
            MessageBox.Show("Приложение 'Библиотека'\nУчебный проект по Windows Forms.", 
                "О программе", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void gridBooks_MouseDown(object? sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                var hit = gridBooks.HitTest(e.X, e.Y);
                if (hit.RowIndex >= 0)
                {
                    gridBooks.ClearSelection();
                    gridBooks.Rows[hit.RowIndex].Selected = true;
                }
            }
        }

        // (Необязательно) Подгрузка по частям
        private void LoadMoreChunk()
        {
            var chunk = FileService.LoadChunk(_loadedCount, PageSize);
            foreach (var b in chunk) _books.Add(b);
            _loadedCount += chunk.Count;
            if (chunk.Count == 0)
                MessageBox.Show("Дополнительных данных нет.", "Загрузка");
        }
    }
