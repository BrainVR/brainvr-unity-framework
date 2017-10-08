Player controller is  an abstract class that is extended by default in teh package by rigidbody player controller. It derives from `Singleton` class, which means it can only have a single instance within each scense.

The idea behinvd abstract classes shoudl solve issues with cusom logging and suctom functions that are implemented differnetly for 2D and 3D screesn as possibly for mobile screens. Therefore functions below are not necessarilly implemented by default adn you are invited to implemtnt them as you see fit when modifying the player contrller in your won project.

# Functions
## MoveToCenter
`public void MoveToCenter()`

Useful in experiments where the beginning of trials is positioned at the Vector3(0, y, 0) coordinates. Moves player to the center position while keeping the y axis constant.

