using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WishingProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private GachaSystem gachaSystem;


        public MainWindow()
        {
            InitializeComponent();
            gachaSystem = new GachaSystem();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DataContext = this; // Set DataContext to the MainWindow instance
        }

        private void PullButton_Click(object sender, RoutedEventArgs e)
        {
            // Trigger the meteor animation
            PlayAnimation("MeteorAnimation");

            // Call the GachaSystem logic here
            (Item obtainedItem, Rarity rarity) = gachaSystem.Pull();

            // Update the UI based on the obtained item
            itemDisplay.Text = $"Obtained Item: {obtainedItem.Name} (Rarity: {obtainedItem.ItemRarity})";

            // Play the corresponding animation based on rarity
            PlayRarityAnimation();
        }
        public string CurrentVideoSource { get; set; }

        private void PlayRarityAnimation()
        {
                CurrentVideoSource = "Material/WishingAnimation/Meteor/5starwish-single.mp4";
            mediaElement.Source = new Uri(CurrentVideoSource, UriKind.RelativeOrAbsolute);
            mediaElement.Play();

        }

        private void PlayAnimation(string animationKey)
        {
            Storyboard animation = (Storyboard)Resources[animationKey];
            if (animation != null)
            {
                meteor.BeginStoryboard(animation);
            }

        }
    }
}
