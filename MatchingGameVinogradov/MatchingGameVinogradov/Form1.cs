using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MatchingGameVinogradov
{
    public partial class Form1 : Form
    {

        // firstClicked указывает на первый элемент управления Label
        // что игрок нажимает, но оно будет нулевым
        // если игрок еще не нажал на метку
        Label firstClicked = null;
        // SecondClicked указывает на второй элемент управления Label
        // что игрок нажимает
        Label secondClicked = null;
        // Используйте этот объект Random, чтобы выбрать случайные значки для квадратов
        Random random = new Random();

        // Каждая из этих букв представляет собой интересный значок
        // в шрифте Webdings,
        // и каждый значок появляется в этом списке дважды
        List<string> icons = new List<string>()
    {
        "!", "!", "N", "N", ",", ",", "k", "k",
        "b", "b", "v", "v", "w", "w", "z", "z"
    };
        private void AssignIconsToSquares()
        {

            // TableLayoutPanel имеет 16 меток,
            // и список значков содержит 16 значков,
            // поэтому значок извлекается из списка случайным образом
            // и добавляем к каждой метке
            foreach (Control control in tableLayoutPanel1.Controls)
            {
                Label iconLabel = control as Label;
                if (iconLabel != null)
                {
                    int randomNumber = random.Next(icons.Count);
                    iconLabel.Text = icons[randomNumber];
                    icons.RemoveAt(randomNumber);
                }
            }
        }
        public Form1()
        {
            InitializeComponent();
            AssignIconsToSquares();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            Label clickedLabel = sender as Label;

            if (clickedLabel != null)
            {
                // Если выбранная метка черная, игрок нажал
                // значок, который уже открыт --
                // игнорировать щелчок
                if (clickedLabel.ForeColor == Color.Black)
                    return;

                // Если firstClicked имеет значение null, это первый значок
                // в паре, на которую нажал игрок,
                // поэтому установите firstClicked на метку, которую игрок
                // нажали, изменяем цвет на черный и возвращаемся
                if (firstClicked == null)
                {
                    firstClicked = clickedLabel;
                    firstClicked.ForeColor = Color.Black;

                    return;
                }

                // Если игрок зайдет так далеко, таймер не сработает
                // запуск и значение firstClicked не равно нулю,
                // так что это должен быть второй значок, на который нажал игрок
                // Устанавливаем черный цвет
                secondClicked = clickedLabel;
                secondClicked.ForeColor = Color.Black;

                // Проверяем, выиграл ли игрок
                CheckForWinner();
                // Если игрок нажал два одинаковых значка, сохраните их
                // черный цвет и сброс firstClicked и SecondClicked
                // чтобы игрок мог щелкнуть другой значок
                if (firstClicked.Text == secondClicked.Text)
                {
                    firstClicked = null;
                    secondClicked = null;
                    return;
                }
                // Если игрок зайдет так далеко, игрок
                // щелкнули два разных значка, поэтому запустим
                // таймер (который будет ждать три четверти
                // секунду, а затем скрываем значки)
                timer1.Start();
            }

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            // Остановить таймер
            timer1.Stop();
            // Скрываем обе иконки
            firstClicked.ForeColor = firstClicked.BackColor;
            secondClicked.ForeColor = secondClicked.BackColor;

            // Сбрасываем firstClicked и SecondClicked
            // поэтому в следующий раз, когда метка будет
            // нажали, программа знает, что это первый клик
            firstClicked = null;
            secondClicked = null;
        }
        private void CheckForWinner()
        {

            // Проходим по всем меткам в TableLayoutPanel,
            // проверяем каждый из них, чтобы увидеть, соответствует ли его значок
            foreach (Control control in tableLayoutPanel1.Controls)
            {
                Label iconLabel = control as Label;

                if (iconLabel != null)
                {
                    if (iconLabel.ForeColor == iconLabel.BackColor)
                        return;
                }
            }

            // Если цикл не вернулся, значит, он не нашел
            // любые несовпадающие значки
            // Это означает, что пользователь выиграл. Показать сообщение и закрыть форму
            MessageBox.Show("У вас совпали все значки, Поздравляю!!");
            Close();
        }
    }
}
