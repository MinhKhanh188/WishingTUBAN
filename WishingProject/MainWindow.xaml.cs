using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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

        private void PullButton_Click(object sender, RoutedEventArgs e)
        {
            // Trigger the meteor animation
            PlayAnimation("MeteorAnimation");

            // Call the GachaSystem logic here
            (Item obtainedItem, Rarity rarity) = gachaSystem.Pull();

            // Update the UI based on the obtained item
            itemDisplay.Text = $"Obtained Item: {obtainedItem.Name} (Rarity: {obtainedItem.ItemRarity})";

            // Play the corresponding animation based on rarity
            PlayRarityAnimation(rarity);
        }



        private void PlayRarityAnimation(Rarity rarity)
        {
            Debug.WriteLine($"Playing {rarity} video.");
            //MediaElement videoElement = (MediaElement)Resources[$"{rarity}Animation"];
            
                videoDisplay.LoadedBehavior = MediaState.Manual;

                // Stop any currently playing video
                videoDisplay.Stop();

                // Set the source of the video display
                videoDisplay.Source = new Uri($"pack://application:,,,/Material/WishingAnimation/Meteor/4starwish.mp4");

                // Play the video
                videoDisplay.Play();
            
        }


        private void StopAllVideos()
        {
            // Stop all rarity videos
            StopVideo("ThreeStarAnimation");
            StopVideo("FourStarAnimation");
            StopVideo("FiveStarAnimation");
        }

        private void StopVideo(string videoKey)
        {
            MediaElement videoElement = (MediaElement)Resources[videoKey];
            if (videoElement != null)
            {
                videoElement.Stop();
            }
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
