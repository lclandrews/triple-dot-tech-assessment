# Triple Dot Tech Assessment

## External Dependencies

- Unity Shader Graph UGUI Shaders Samples
	- Specifically the sub nodes library

## Key Features

- Implementation of Level Complete screen with Shaders, Particles & Animation

- Dynamic positioning to adapt to safe zone

- Animated pill toggles

- Shader driven buttons & icons featuring animated sheen

- Functional implementation of text localization

## Desired Improvements

- Implement placeholder Shop & Map screens with slide in / out animations from left
	& right respectively to be activated by nav bar actions

- Implement blur in a more scalable way utilising SRP render features and spend more time
	exploring how to actively blur from any given point within the UI hierarchy
	- Alternatively consider the implementation of a stable 3rd party package for UI blurring

- Implement a more robust solution for nav bar buttons to prevent state animations from
	being executed unnecessarily
	- Potentially implement a class derived from either button or toggle to accurately
		visualise the desired 'Active' state and override the DoStateTransition
		method to capture desired behaviour... (Needs further consideration)

- Automate the setting of the ON & OFF positions of the pill toggle instead of relying
	on serialized editor properties

- Create a more robust and scalable solution for arbitrarily repositioning UI elements
	to render within the safe area

- Spend more time authoring exported graphics from photoshop and optimising to improve
	memory footprint (lower resolution & more aggressive 9 slicing)

- More polish and attention spent on font settings to better represent the reference
	materials

- Project asset naming conventions could be improved and more consistent, implementing rigid rules, e.g:
	Type_Variant_Asset_Variant : S(prite)_Blue_Button_Small
	- The ideal is to ensure (or attempt to) that assets are quickly identifiable by their short name 
		as displayed in the project window

- More prevalent usage of animated materials for interactive UI elements

- Code structure is lacking, no clearly defined framework of UI components, some simple interfaces created
	for extension but in it's current implementation it would not provide a solid foundation for a 
	fully fledged application

- Improved runtime response to resolution and safe zone adjustments

- Button animations for the standard buttons could be improved for state transitions, currently they all perform the same generic scale effect, this could be refined.

- Level completed font is not 100% accurate representation of reference materials

## WIP

- Better utilised procedural generation of shapes within button shader to fully generate
	the graphic instead of the partial shader driven approach demonstrated
    - This was started and in progress: M_Canvas_Button_Blue_V2 & M_Canvas_Button_Green_V2
    materials demo the shader, however it was not completed in time and the results were not
    as good as the pre authored graphic combine with the V1 shader

## Issues

- All interactive elements are set to animate highlighted, pressed, & selected states. This covers the brief but does not look right on mobile / simulator highlighted states don't contextually make sense

- UI scales well for all simulator devices but not the Galaxy Z fold due to the small width of the device, this could be refined and all components updated accordingly but would take additional time

- German words are long and the UI should adapt to larger strings better than currently implemented

- TMP Material presets do not appear to be working as expected to unfortunately each instance of a unique font is a separate font asset and not a material preset

## Time Log
- ~ 18 - 20 Hours approximately
