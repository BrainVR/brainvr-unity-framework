# Arena object controller
## Show
`public void Show(bool bo = true)`

Iterates thorugh all renderers and colliders of the `ActiveObject` and sets them to the `bo` parameter. Keeps trigger colliders active regardless.

## Hide
`public void Hide()`

Same as `Show(false)`. Iterates through renderers and colliders on the `ActiveObject` and disables them.

## Set rotation
`void SetRotation(Quaternion rotation)`

Sets `ActiveObject` to designated rotation.

## StartRotation
`void StartRotation(Vector3 direction, float speed)`

Starts rotation in direction at a given `speed`. If rotation is undergoing, it gets stopped and then reinstantiated. Direction of the rotation can be set using negative and positive vectors.

## StopRotation
`void StopRotation()`

If the object was rotating, it stops.

## SetSize
`void SetSize(Vector3 scale)`

Sets the scaling of the `ActiveObject` itself. Doesn't change scale of the controller object itself.

## SetColor
`void SetColor(Color color)`

Sets the main colour of the renderer material of hte Active object to designated colour. Any reset of colour is done towards the colour the object had during OnEnable call.

## SetType
`void SetType(string s, bool force = false)`

Sets the type of the object to one of children objects of the same name. 

## Switch
`public bool Switch()`

Calls Show if the object is hidden and Hide if the object is shown. Retuns bool with the current state after the change.
