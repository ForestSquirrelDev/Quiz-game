
https://user-images.githubusercontent.com/82777171/128170297-169ab18b-9770-4dcb-b6de-5f1e41c075e9.mp4


# Download Unitypackage
- [Link](https://github.com/ForestSquirrelDev/TestAssignment/blob/master/Packages/LZ-TestAssignment.unitypackage) - this version is slightly adjusted for default PC build settings. Original project was made with Android build settings

## Few short remarks
### Events
Whole component structure of this project is heavily tied to events, both Unity and C#.

The greatest advantage of UnityEvents is the visualization in Editor - you can simply drag and drop desired listeners with no need of writing the code, which is very flexible if you don't know how many observers you want to have.

But at the same time relations between components become more and more unobvious as the number of those dragged listeners increases, whereas with C# events you can track every listener directly in the code. Because of that, i tried to use C# events where there's no need in visualisation and number of observers is meant to be fixed.

### Code comments
Though there is a very popular opinion that clean code doesn't need to be commented, i tend to believe that a short summary over a class or method, or even few lines of comments can speed up reading the code a lot, especially when this code is far from being perfect.
