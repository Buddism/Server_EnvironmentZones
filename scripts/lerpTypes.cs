//LERP HANDLING:
// call Environment::setClientEnv with an environmentZone to skip the transition process entirely
// call Environment::cancelTransition to do nothing (unused)
// call Environment::initTransition to initialize the lerp values
// call Environment::transitionEnvironment with a 0 to 1 float

function Environment::TimeTransition(%this, %other, %time, %start)
{
	if(!isObject(%other))
		return;

	if(%start == 0)
	{
		if(isEventPending(%this.transitionSchedule))
			%this.cancelTransition(%other);

		if(%time == 0 || !%this.hasCreatedEnvironment)
			return %this.setClientEnv(%other);

		%start = getSimTime();
		%hasLerps = %this.initTransition(%other);

		cancel(%this.transitionSchedule);
	}

	%lerp = (getSimTime() - %start) / %time;
	//cos lerp
	if(%lerp < 1)
		%lerp = (1 - mCos(%lerp * $PI)) / 2;

	%this.transitionEnvironment(%other, %lerp);

	%this.client.bottomPrint(%lerp NL %this.sun.color, 1, 1);
	if(%lerp < 1)
		%this.transitionSchedule = %this.schedule(31, TimeTransition, %other, %time, %start);
}