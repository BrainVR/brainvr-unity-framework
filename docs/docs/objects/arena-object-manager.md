# Description

# Functions
## ShowAll
`public void ShowAll(bool bo = true)`

Iterates thorugh all objects and calls `Show(bo)` at each controller. See [Show](objects/arenaobject.md#show).

## Hide
`public void HideAll()`

Same as `ShowAll(false)`. Runs Hide on all arena object controllers. See [Hide](objects/arenaobject.md#hide).

## SetColor
`void SetColor(Color color)`

Sets colour of each obejct material if possible. See [SetColor](objects/arena-object.md#setcolor).

## SetType
`void SetType(string s, bool force = false)`¨

Iterates though objects and sets each object ot appropriate type as possible. See [SetType](objects/arena-object.md#settype).