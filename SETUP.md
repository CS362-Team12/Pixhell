### Building

The following instructions assume that Pixhell has already been launched through Unity and the Unity Editor is open. If not, the instructions for that are located above.

  

1.  Open Pixhell in Unity
    
2.  Ensure that in scripts/GameConstants.cs DEBUG is set to false. Leaving DEBUG on can cause some debug effects, such as increased drop rates and forced upgrades, to be on. Changing this variable to false will force these effects to be off.
    
3.  For official builds (non beta), any save files should also be deleted from the local machine. To do this, navigate to Assets/StreamingAssets/Runs and delete all files there. Run the project in Unity to ensure that no runs are saved.
    
4.  Go to File > Build Profiles
    

![](https://lh7-rt.googleusercontent.com/docsz/AD_4nXerqC-RqZ5FxrSkFZKhvBdTvqWDP0cCme_08laNXuvLOKuFRfG3HldLXL7zVyuqnm3LdtXCwRL3_u4XN3XxjgCbklJx7dLOR0ln-0ztWrMleqx6c5wLgjN5H7sT3EnIkPJbvWIgcw?key=4Yrl1txNmBFwXurP5N_Ljrbf)

5.  Check that the Scene List contains all the scenes included in the game. If necessary, click “Add Open Scenes” to include all the open scenes. See the image below for scenes from the last update.
    

  
![](https://lh7-rt.googleusercontent.com/docsz/AD_4nXfXfbJZrPMvhB4I7UVyOnaVslNuF4TviqOEq8vTjngVCSqgydvjhxM2Q_tFW47Io1Nxr7Du55k1h7GRH537KGJHXe45Lc1HkRniQDKbUly3aYRAEzVaaMXmjUMlnjycXQRilXXpiA?key=4Yrl1txNmBFwXurP5N_Ljrbf)

6.  Windows:
    

1.  Find “Windows Build”
    

![](https://lh7-rt.googleusercontent.com/docsz/AD_4nXe5W7KhENbyIeAAtgDiIkINIYFRSwDS5U-VXt0OEPnkoDf4P9fYeK9KcIXzh0RQNRMKRfEuU1d1KaEL9dydvEdpzXrqGSr4WOURtqBySndBN-OtheWlyjtrUlweOE4-Uh9ddtzF?key=4Yrl1txNmBFwXurP5N_Ljrbf)

2.  Click “Switch Profile” to make the Windows Build active
    

![](https://lh7-rt.googleusercontent.com/docsz/AD_4nXcCfBV-0rXKxDq_lPNJOpyiwxG5DngjrScmLyP3Hz3oZm0zDcslFQwuuyKlXERsqFS0fknSJbNZMgu3ZqRSYfaREHh0VrmbBSycvcNLrLaQK-jg-FFCGQ0BlEEmbioVT6Z6Wu_0Bw?key=4Yrl1txNmBFwXurP5N_Ljrbf)

![](https://lh7-rt.googleusercontent.com/docsz/AD_4nXcV2bfqHleC9zUIZO-_8A0M7AKvJV1uDuEUpZewEvTyhOzch0rxbT4PLFWb0FFjnJS3o2oUJMxowFVko4hlzVCuiZJL58PLWIocII_xZezd8I2JryafMGeCJgHkXHNiUYpaThjOwQ?key=4Yrl1txNmBFwXurP5N_Ljrbf)

7.  Mac:
    

1.  Find “Mac Build”
    

![](https://lh7-rt.googleusercontent.com/docsz/AD_4nXfYjxV6IvsgM3-LKZCY_n1SO8JhpatZCHtApHCzdCo0Z2WtgwWuuzIlwMSu3HuGEB0qg113NSIY-TdZdh3hPHTIfFfZSMBFrSByuP9KSe25bpjpxS5Z2lxRaH1cFTmA0kGPigp6ZQ?key=4Yrl1txNmBFwXurP5N_Ljrbf)

2.  Click “Switch Profile” to make the Mac Build active
    

![](https://lh7-rt.googleusercontent.com/docsz/AD_4nXdQEBxS71AuzC439dfX4FV9xwHZaR9WPNxi4OOHhywucIcWhEAxe0TiRYPOTKEqQYp-o2DwnJNxxYd1qLR83Qu1VX3akUcn41FyYlpq4YhvnYX1fa_4kLp5LBLNHNHOrSgQYtfkng?key=4Yrl1txNmBFwXurP5N_Ljrbf)

![](https://lh7-rt.googleusercontent.com/docsz/AD_4nXfWY1lzJ_uYPKbm7-3TuT1lOxz2DfH1YSzJMbxgE1rpTddS5f-suBAd9_eOpNgTjXBSbae9rAsdfH17lvu6x6W-UHGQ4a5Nw_23CYa3uEe9DpVZ4en3PU58lEC_KWM2qlzFAaEx?key=4Yrl1txNmBFwXurP5N_Ljrbf)

8.  Click Build on your chosen operating system, and select a folder that is easy to find.
    

1.  If you get the error pictured below, click “Yes” to continue. The project does not rely on Unity services and the build should continue without it.
    

10.  Navigate to the folder the game got built (in some cases, the folder will automatically open). Run pixhell.exe to ensure it works. ![](https://lh7-rt.googleusercontent.com/docsz/AD_4nXeK0bfEzeTwQVq7n6gM3RGdlIW6Q1irkdGlqiRKv2X0SvXfNwYTAoclB1Wd4NxmAMKvvxqWjdRrAONOBMLSwyfTu-XE1IERNIBkasc4KtzwjHB7gcicwJlN_drqDjQsRvxGa6S6Ig?key=4Yrl1txNmBFwXurP5N_Ljrbf)
