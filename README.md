
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

### Scene management decisions

![image](https://user-images.githubusercontent.com/82777171/128204042-6452ec02-2729-4ce4-9b24-89ceb88bb032.png)
![image](https://user-images.githubusercontent.com/82777171/128202708-3f24eeee-91fd-4dfc-9061-392c8e2786c3.png)
![image](https://user-images.githubusercontent.com/82777171/128204221-48f711d8-dc98-4c63-a96e-8075bd006dbf.png)

In order to keep my project structure as clean as possible, i broke vast majority of GameObjects into two logical groups: UI Canvas and Game logic components.

In my code structure i've made an attempt to minimize cross-component dependencies and make classes that only do one particular thing.
If it's a quiz class, it manages quiz flow, notices all observers about how the quiz is going and it does not care what other classes are doing at the moment (except for the events that the Quiz class is subscribed on). If the class is responsible for some kind of tweening, it only does this particular thing - it tweens. 

And in the scene hierarchy i've tried to keep this mentality of separating things up so the workflow with this project's architecture could be as convenient as possible.
