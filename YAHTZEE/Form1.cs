using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace YAHTZEE
{
    public partial class Form1 : Form
    {
        private List<int> dices = new List<int>() { -1, -1, -1, -1, -1, };
        private int pocket_roll = 3;
        private int total_score = 0;
        private bool selected = false;
        private bool game_end = false;
        public Form1()
        {
            InitializeComponent();
            btn_remain_roll.Text = pocket_roll.ToString();
        }

        private void btn_roll_Click(object sender, EventArgs e)
        {
            Random rnd = new Random();
            _ = rnd;

            if (game_end == true)
            {
                NewGame();
                game_end = false;
            }

            pocket_roll--;
            if (pocket_roll == 0)
            {
                btn_roll.Enabled = false;
            }
            if (pocket_roll == -1)
            {
                pocket_roll = 3;

                Dice_Reset(btn_dice_one);
                Dice_Reset(btn_dice_two);
                Dice_Reset(btn_dice_three);
                Dice_Reset(btn_dice_four);
                Dice_Reset(btn_dice_five);
                btn_remain_roll.Text = pocket_roll.ToString();

                Reset();

                return;
            }
            else
            {
                foreach (Control ctrl in pnl_table.Controls)
                {
                    ctrl.Enabled = true;
                }
            }

            foreach (Control ctrl in pnl_table.Controls)
            {
                if (ctrl.GetType() == typeof(Button))
                {
                    ctrl.Text = rnd.Next(1, 7).ToString();
                }
            }

            btn_remain_roll.Text = pocket_roll.ToString();
            selected = false;

            dices[0] = int.Parse(btn_dice_one.Text);
            dices[1] = int.Parse(btn_dice_two.Text);
            dices[2] = int.Parse(btn_dice_three.Text);
            dices[3] = int.Parse(btn_dice_four.Text);
            dices[4] = int.Parse(btn_dice_five.Text);


            Reset();
            Upper_Section();
            Lower_Section();
            All_Zero();
        }
        private void Reset()
        {
            int empty_count = 0;
            foreach (Control ctrl in tlp_lower.Controls)
            {
                if (ctrl.GetType() == typeof(Button))
                {
                    if (ctrl.Text == "")
                    {
                        empty_count++;
                    }
                }
            }
            foreach (Control ctrl in tlp_upper.Controls)
            {
                if (ctrl.GetType() == typeof(Button))
                {
                    if (ctrl.Text == "")
                    {
                        empty_count++;
                    }
                }
            }

            if (empty_count == 0 && game_end == false)
            {
                game_end = true;
                GameEnd();
            }

            Dice_Reset(btn_dice_one);
            Dice_Reset(btn_dice_two);
            Dice_Reset(btn_dice_three);
            Dice_Reset(btn_dice_four);
            Dice_Reset(btn_dice_five);

            foreach (Control ctrl in tlp_lower.Controls)
            {
                if (ctrl.GetType() == typeof(Button))
                {
                    if (!ctrl.Font.Bold)
                    {
                        ctrl.Enabled = false;
                        ctrl.ForeColor = SystemColors.ControlText;
                        ctrl.ResetText();
                    }
                }
            }
            foreach (Control ctrl in tlp_upper.Controls)
            {
                if (ctrl.GetType() == typeof(Button))
                {
                    if (!ctrl.Font.Bold)
                    {
                        ctrl.Enabled = false;
                        ctrl.ForeColor = SystemColors.ControlText;
                        ctrl.ResetText();
                    }
                }
            }
        }
        private void All_Zero()
        {
            int Hit_Count = 0;

            foreach (Control ctrl in tlp_lower.Controls)
            {
                if (ctrl.GetType() == typeof(Button))
                {
                    if (ctrl.ForeColor == Color.Red)
                    {
                        Hit_Count++;
                    }
                }
            }

            foreach (Control ctrl in tlp_upper.Controls)
            {
                if (ctrl.GetType() == typeof(Button))
                {
                    if (ctrl.ForeColor == Color.Red)
                    {
                        Hit_Count++;
                    }
                }
            }

            if (Hit_Count == 0)
            {
                foreach (Control ctrl in tlp_upper.Controls)
                {
                    if (ctrl.GetType() == typeof(Button))
                    {
                        if (!ctrl.Font.Bold && ctrl.Text == "")
                        {
                            ctrl.Enabled = true;
                            ctrl.ForeColor = Color.Red;
                            ctrl.Text = "0";
                        }
                    }
                }

                foreach (Control ctrl in tlp_lower.Controls)
                {
                    if (ctrl.GetType() == typeof(Button))
                    {
                        if (!ctrl.Font.Bold && ctrl.Text == "")
                        {
                            ctrl.Enabled = true;
                            ctrl.ForeColor = Color.Red;
                            ctrl.Text = "0";
                        }
                    }
                }
            }
        }
        private void Upper_Section()
        {
            if (dices.Contains(1) && !btn_ones.Font.Bold)
            {
                btn_ones.Enabled = true;
                btn_ones.ForeColor = Color.Red;

                int Sum = dices.Count(x => x == 1) * 1;
                btn_ones.Text = Sum.ToString();
            }
            if (dices.Contains(2) && !btn_twos.Font.Bold)
            {
                btn_twos.Enabled = true;
                btn_twos.ForeColor = Color.Red;

                int Sum = dices.Count(x => x == 2) * 2;
                btn_twos.Text = Sum.ToString();
            }
            if (dices.Contains(3) && !btn_threes.Font.Bold)
            {
                btn_threes.Enabled = true;
                btn_threes.ForeColor = Color.Red;

                int Sum = dices.Count(x => x == 3) * 3;
                btn_threes.Text = Sum.ToString();
            }
            if (dices.Contains(4) && !btn_fours.Font.Bold)
            {
                btn_fours.Enabled = true;
                btn_fours.ForeColor = Color.Red;

                int Sum = dices.Count(x => x == 4) * 4;
                btn_fours.Text = Sum.ToString();
            }
            if (dices.Contains(5) && !btn_fives.Font.Bold)
            {
                btn_fives.Enabled = true;
                btn_fives.ForeColor = Color.Red;

                int Sum = dices.Count(x => x == 5) * 5;
                btn_fives.Text = Sum.ToString();
            }
            if (dices.Contains(6) && !btn_sixes.Font.Bold)
            {
                btn_sixes.Enabled = true;
                btn_sixes.ForeColor = Color.Red;

                int Sum = dices.Count(x => x == 6) * 6;
                btn_sixes.Text = Sum.ToString();
            }
        }
        private void btn_upper_click(object sender, EventArgs e)
        {
            Button btn_sender = sender as Button;
            btn_sender.ForeColor = SystemColors.ControlText;
            btn_sender.Font = new Font(btn_sender.Font, FontStyle.Bold);
            btn_sender.Enabled = false;

            int BoldCount = 0;
            foreach (Control ctrl in tlp_upper.Controls)
            {
                if (ctrl.GetType() == typeof(Button))
                {
                    ctrl.Enabled = false;
                    if (!ctrl.Font.Bold)
                    {
                        ctrl.ForeColor = SystemColors.ControlText;
                        ctrl.ResetText();
                    }
                    else
                    {
                        BoldCount++;
                    }
                }
            }
            if (BoldCount == 6)
            {
                int sum = 0;
                bool ContainsZero = false;
                foreach (Control ctrl in tlp_upper.Controls)
                {
                    if (ctrl.GetType() == typeof(Button))
                    {
                        sum += int.Parse(ctrl.Text);
                        if (ctrl.Text == "0")
                        {
                            ContainsZero = true;
                            btn_bonus.Text = "0";
                        }
                    }
                }
                if (!ContainsZero && sum >= 63)
                {
                    btn_bonus.Text = "35";
                }
                else
                {
                    btn_bonus.Text = "0";
                }
                btn_sum.Text = sum.ToString();
            }

            btn_roll.Enabled = true;
            selected = true;
            btn_remain_roll.Text = "3";
            pocket_roll = 3;
            Reset();
        }
        private void Lower_Section()
        {
            Yahtzee();
            Chance();
            LargeStraight();
            SmallStraight();
            FullHouse();
            TwoKind();
            ThreeKind();
            FourKind();
        }
        private void FourKind()
        {
            if (
                dices.Distinct().Count() <= 2 && !btn_four_kind.Font.Bold &&
                    (
                        dices.Count(x => x == dices.Min()) == 4 ||
                        dices.Count(x => x == dices.Max()) == 4
                    )
               )
            {
                btn_four_kind.Enabled = true;
                btn_four_kind.ForeColor = Color.Red;
                btn_four_kind.Text = dices.Sum().ToString();
            }
        }
        private void ThreeKind()
        {
            if (dices.GroupBy(x => x).Any(g => g.Count() >= 3) && !btn_three_kind.Font.Bold)
            {
                btn_three_kind.Enabled = true;
                btn_three_kind.ForeColor = Color.Red;
                btn_three_kind.Text = dices.Sum().ToString();
            }
        }
        private void TwoKind()
        {
            if (dices.GroupBy(x => x).Any(g => g.Count() >= 2) && !btn_two_kind.Font.Bold)
            {
                btn_two_kind.Enabled = true;
                btn_two_kind.ForeColor = Color.Red;
                btn_two_kind.Text = dices.Sum().ToString();
            }
        }
        private void FullHouse()
        {
            if (dices.Distinct().Count() == 2 && !btn_full.Font.Bold &&
                    (
                        (
                            (dices.Count(x => x == dices.Min()) == 2 &&
                            dices.Count(x => x == dices.Max()) == 3)
                        ) ||
                        (
                            (dices.Count(x => x == dices.Min()) == 3 ||
                            dices.Count(x => x == dices.Max()) == 2)
                        )
                    )
                )
            {
                btn_full.Enabled = true;
                btn_full.ForeColor = Color.Red;
                btn_full.Text = "25";
            }
        }
        private void SmallStraight()
        {
            if (
                    dices.Distinct().Count() >= 4 && !btn_small.Font.Bold &&
                    dices.Contains(3) &&
                    dices.Contains(4) &&
                    (
                        (dices.Contains(1) && dices.Contains(2)) ||
                        (dices.Contains(2) && dices.Contains(5)) ||
                        (dices.Contains(5) && dices.Contains(6))

                    )
                )
            {

                btn_small.Enabled = true;
                btn_small.ForeColor = Color.Red;
                btn_small.Text = "30";
            }
        }
        private void LargeStraight()
        {
            if (
                    dices.Distinct().Count() == 5 && !btn_large.Font.Bold &&
                    (
                        (dices.Distinct().Min() == 1 && dices.Distinct().Max() == 5) ||
                        (dices.Distinct().Min() == 2 && dices.Distinct().Max() == 6)
                    )
                )
            {
                btn_large.Enabled = true;
                btn_large.ForeColor = Color.Red;
                btn_large.Text = "40";
            }
        }
        private void Chance()
        {
            if (!btn_chance.Font.Bold)
            {
                btn_chance.Enabled = true;
                btn_chance.ForeColor = Color.Red;
                btn_chance.Text = dices.Sum().ToString();
            }

        }
        private void Yahtzee()
        {
            if (dices.Distinct().Count() == 1 && !btn_yahtzee.Font.Bold)
            {
                btn_yahtzee.Enabled = true;
                btn_yahtzee.ForeColor = Color.Red;
                btn_yahtzee.Text = "50";
            }
        }
        private void btn_lower(object sender, EventArgs e)
        {
            Button btn_sender = sender as Button;
            btn_sender.ForeColor = SystemColors.ControlText;
            btn_sender.Font = new Font(btn_sender.Font, FontStyle.Bold);
            btn_sender.Enabled = false;

            foreach (Control ctrl in tlp_lower.Controls)
            {
                if (ctrl.GetType() == typeof(Button))
                {
                    ctrl.Enabled = false;
                    if (!ctrl.Font.Bold)
                    {
                        ctrl.ForeColor = SystemColors.ControlText;
                        ctrl.ResetText();
                    }
                }
            }

            total_score += int.Parse(btn_sender.Text);
            btn_roll.Enabled = true;
            selected = true;
            btn_remain_roll.Text = "3";
            pocket_roll = 3;

            Reset();
        }

        private void btn_dice_click(object sender, EventArgs e)
        {
            Button btn_sender = sender as Button;
            if (pnl_table.Controls.Contains(btn_sender))
            {
                btn_sender.Location = new Point(btn_sender.Location.X, 10);
                pnl_table.Controls.Remove(btn_sender);
                pnl_pocket.Controls.Add(btn_sender);
            }
            else
            {
                btn_sender.Location = new Point(btn_sender.Location.X, 4);
                pnl_pocket.Controls.Remove(btn_sender);
                pnl_table.Controls.Add(btn_sender);
            }
            if (pnl_table.Controls.Count != 0 && pocket_roll > 0)
            {
                btn_roll.Enabled = true;
            }
        }
        private void Dice_Reset(Button dice_button)
        {
            if (selected)
            {
                dice_button.Enabled = false;
                dice_button.Location = new Point(dice_button.Location.X, 4);
                pnl_table.Controls.Add(dice_button);
                pnl_pocket.Controls.Remove(dice_button);
            }
            else
            {
                dice_button.Enabled = true;
            }
        }

        private void NewGame()
        {
            lbl_score.ResetText();
            btn_remain_roll.Text = "3";
            pocket_roll = 3;
            btn_sum.ResetText();
            btn_bonus.ResetText();

            foreach (Control ctrl in pnl_table.Controls)
            {
                ctrl.Enabled = false;
                ctrl.Location = new Point(ctrl.Location.X, 4);
                pnl_table.Controls.Add(ctrl);
                pnl_pocket.Controls.Remove(ctrl);
            }

            foreach (Control ctrl in tlp_lower.Controls)
            {
                if (ctrl.GetType() == typeof(Button))
                {
                    ctrl.ForeColor = SystemColors.ControlText;
                    ctrl.ResetText();
                }
            }

            foreach (Control ctrl in tlp_upper.Controls)
            {
                if (ctrl.GetType() == typeof(Button))
                {
                    ctrl.ForeColor = SystemColors.ControlText;
                    ctrl.ResetText();
                }
            }
        }

        private void GameEnd()
        {   
            total_score += int.Parse(btn_sum.Text);
            total_score += int.Parse(btn_bonus.Text);

            lbl_score.Text = total_score.ToString();
        }
    }
}
