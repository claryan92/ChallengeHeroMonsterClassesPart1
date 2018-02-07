using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ChallengeHeroMonsterClassesPart1
{
	public partial class Default : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			Character hero = new Character();
			
			hero.Name = "Moana";
			hero.Health = 150;
			hero.DamageMaximum = 25;
			hero.AttackBonus = true;

			Character monster = new Character();
			monster.Name = "Ursula";
			monster.Health = 200;
			monster.DamageMaximum = 20;
			monster.AttackBonus = false;

			Dice dice = new Dice();

			//Bonus
			if (hero.AttackBonus)
				monster.Defend(hero.Attack(dice));
			if (monster.AttackBonus)
				hero.Defend(monster.Attack(dice));

			while (hero.Health > 0 && monster.Health > 0)
			{
				monster.Defend(hero.Attack(dice));
				hero.Defend(monster.Attack(dice));

				displayStats(hero);
				displayStats(monster);
			}

			displayResult(hero, monster);

		}

		private void displayResult(Character opponent1, Character opponent2)
		{
			if (opponent1.Health <= 0 && opponent2.Health <= 0)
				resultLabel.Text += String.Format("<p>Both {0} and {1} died.</p>", opponent1.Name, opponent2.Name);
			else if (opponent1.Health <= 0)
				resultLabel.Text += String.Format("<p>{0} defeats {1}.</p>", opponent2.Name, opponent1.Name);
			else
				resultLabel.Text += String.Format("<p>{0} defeats {1}.</p>", opponent1.Name, opponent2.Name);
		}

		private void displayStats(Character character)
		{
			resultLabel.Text += String.Format("<p>Name: {0} - Health: {1} - DamageMaximum: {2} - AttackBonus: {3}</p>", 
				character.Name, 
				character.Health, 
				character.DamageMaximum, 
				character.AttackBonus);
		}
	}

	class Character
	{
		public string Name { get; set; }
		public int Health { get; set; }
		public int DamageMaximum { get; set; }
		public bool AttackBonus { get; set; }

		public int Attack(Dice dice)
		{
			/*
			Random random = new Random();
			int damage = random.Next(this.DamageMaximum);
			*/
			dice.Sides = this.DamageMaximum;
			return dice.Roll();
		}

		public void Defend(int damage)
		{
			this.Health -= damage;
		}
	}

	class Dice
	{
		public int Sides { get; set; }

		Random random = new Random();
		public int Roll()
		{
			return random.Next(this.Sides);
		}
	}
}