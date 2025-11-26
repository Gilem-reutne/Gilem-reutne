using System.ComponentModel;
using System.Windows.Forms;

namespace LibraryApp;

public class Form1 : Form
{
    private BindingList<Book> _books = new();
    private DataGridView gridBooks = null!;
    private MenuStrip menuStrip = null!;
    private Button btnAdd = null!;
    private Button btnEdit = null!;
    private Button btnDelete = null!;

    public Form1()
    {
        InitializeUI();
        Load += Form1_Load;
    }

    private void InitializeUI()
    {
        Text = "Библиотека";
        ClientSize = new System.Drawing.Size(800, 500);
        StartPosition = FormStartPosition.CenterScreen;

        // MenuStrip
        menuStrip = new MenuStrip();
        
        var fileMenu = new ToolStripMenuItem("Файл");
        var exitItem = new ToolStripMenuItem("Выход", null, (s, e) => Close());
        fileMenu.DropDownItems.Add(exitItem);

        var editMenu = new ToolStripMenuItem("Правка");
        var addItem = new ToolStripMenuItem("Добавить", null, (s, e) => AddBook());
        var editItem = new ToolStripMenuItem("Редактировать", null, (s, e) => EditBook());
        var deleteItem = new ToolStripMenuItem("Удалить", null, (s, e) => DeleteBook());
        editMenu.DropDownItems.AddRange(new ToolStripItem[] { addItem, editItem, deleteItem });

        var helpMenu = new ToolStripMenuItem("Справка");
        var aboutItem = new ToolStripMenuItem("О программе", null, (s, e) =>
            MessageBox.Show("Приложение 'Библиотека'\nУчебный проект по Windows Forms.", "О программе", MessageBoxButtons.OK, MessageBoxIcon.Information));
        helpMenu.DropDownItems.Add(aboutItem);

        menuStrip.Items.AddRange(new ToolStripItem[] { fileMenu, editMenu, helpMenu });
        MainMenuStrip = menuStrip;
        Controls.Add(menuStrip);

        // DataGridView
        gridBooks = new DataGridView
        {
            Location = new System.Drawing.Point(0, 24),
            Size = new System.Drawing.Size(800, 400),
            Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right,
            ReadOnly = true,
            AllowUserToAddRows = false,
            SelectionMode = DataGridViewSelectionMode.FullRowSelect,
            MultiSelect = false,
            AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        };
        gridBooks.MouseDown += GridBooks_MouseDown;
        Controls.Add(gridBooks);

        // Context menu for DataGridView
        var contextMenu = new ContextMenuStrip();
        contextMenu.Items.Add("Добавить", null, (s, e) => AddBook());
        contextMenu.Items.Add("Редактировать", null, (s, e) => EditBook());
        contextMenu.Items.Add("Удалить", null, (s, e) => DeleteBook());
        gridBooks.ContextMenuStrip = contextMenu;

        // Bottom panel with buttons
        var panel = new Panel
        {
            Height = 50,
            Dock = DockStyle.Bottom
        };

        btnAdd = new Button { Text = "Добавить", Location = new System.Drawing.Point(10, 10), Width = 100 };
        btnEdit = new Button { Text = "Редактировать", Location = new System.Drawing.Point(120, 10), Width = 100 };
        btnDelete = new Button { Text = "Удалить", Location = new System.Drawing.Point(230, 10), Width = 100 };

        btnAdd.Click += (s, e) => AddBook();
        btnEdit.Click += (s, e) => EditBook();
        btnDelete.Click += (s, e) => DeleteBook();

        panel.Controls.Add(btnAdd);
        panel.Controls.Add(btnEdit);
        panel.Controls.Add(btnDelete);
        Controls.Add(panel);
    }

    private void Form1_Load(object? sender, EventArgs e)
    {
        var loaded = FileService.Load();
        _books = new BindingList<Book>(loaded);
        gridBooks.DataSource = _books;
        ConfigureGridColumns();
    }

    private void ConfigureGridColumns()
    {
        if (gridBooks.Columns["Id"] != null) gridBooks.Columns["Id"].Visible = false;
        if (gridBooks.Columns["Title"] != null) gridBooks.Columns["Title"].HeaderText = "Название";
        if (gridBooks.Columns["Author"] != null) gridBooks.Columns["Author"].HeaderText = "Автор";
        if (gridBooks.Columns["Year"] != null) gridBooks.Columns["Year"].HeaderText = "Год";
        if (gridBooks.Columns["Price"] != null) gridBooks.Columns["Price"].HeaderText = "Цена";
        if (gridBooks.Columns["Genre"] != null) gridBooks.Columns["Genre"].HeaderText = "Жанр";
        if (gridBooks.Columns["InStock"] != null) gridBooks.Columns["InStock"].HeaderText = "В наличии";
    }

    protected override void OnFormClosing(FormClosingEventArgs e)
    {
        FileService.Save(_books.ToList());
        base.OnFormClosing(e);
    }

    private void AddBook()
    {
        using var editForm = new EditForm();
        if (editForm.ShowDialog() == DialogResult.OK)
        {
            _books.Add(editForm.ResultBook);
        }
    }

    private void EditBook()
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

    private void DeleteBook()
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

    private void GridBooks_MouseDown(object? sender, MouseEventArgs e)
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
}
