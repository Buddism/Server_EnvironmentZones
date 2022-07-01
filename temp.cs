//%other is a zone environment (template for the client zone)
function Environment::setFrom(%this, %other)
{
	if(!isObject(%other) || !%this.isClientEnv)
		return;
	
	//SUN
	if(isObject(%this.sun))
	{
		%this.sun.ambient = %other.var_AmbientLightColor;
		%this.sun.azimuth = %other.var_SunAzimuth;
		%this.sun.color = %other.var_DirectLightColor;
		%this.sun.elevation = %other.var_SunElevation;
		%this.sun.shadowColor = %other.var_ShadowColor;
	} else {
		%name = %other.sun.getName();
		%other.sun.setName("Template");
		%this.sun = new Sun(Copy : Template).getId();
		%this.sun.setName("");
		%other.sun.setName(%name);
	}
	
	//SUN LIGHT
	if(isObject(%this.sunLight))
	{
		%this.sunLight.FlareSize = %other.var_SunFlareSize;
		%this.sunLight.color = %other.var_SunFlareColor;
		%this.sunLight.setFlareBitmaps ($EnvGuiServer::SunFlare[%other.var_SunFlareTopIdx],$EnvGuiServer::SunFlare[%other.var_SunFlareBottomIdx]);
	} else {
		%name = %other.sunLight.getName();
		%other.sunLight.setName("Template");
		%this.sunLight = new FxSunLight(Copy : Template).getId();
		%this.sunLight.setName("");
		%other.sunLight.setName(%name);
	}
	
	//SKY
	if(isObject(%this.sky))
	{
		%this.sky.visibleDistance = %other.var_VisibleDistance;
		%this.sky.fogDistance = %other.var_FogDistance;
		%this.sky.fogColor = getColorF (%other.var_FogColor);
		%this.sky.skyColor = getColorF (%other.var_SkyColor);
		%this.sky.windVelocity = %other.var_WindVelocity;
		%this.sky.windEffectPrecipitation = %other.var_WindEffectPrecipitation;
	} else {
		%name = %other.sky.getName();
		%other.sky.setName("Template");
		%this.sky = new Sky(Copy : Template).getId();
		%this.sky.setName("");
		%other.sky.setName(%name);
	}

	if(isObject(%this.groundPlane))
	{
		%this.groundPlane.setTransform(%other.groundPlane.getTransform());
		%this.groundPlane.color = getColorI (%other.var_GroundColor);
		%this.groundPlane.blend = getWord (%this.groundPlane.color, 3) < 255;
		%this.groundPlane.scrollSpeed = %other.var_GroundScrollX SPC %other.var_GroundScrollY;
	} else {
		%name = %other.groundPlane.getName();
		%other.groundPlane.setName("Template");
		%this.groundPlane = new FxPlane(Copy : Template).getId();
		%this.groundPlane.setName("");
		%other.groundPlane.setName(%name);
	}

	//does the other have a waterplane?
	if(isObject(%other.waterPlane))
	{
		if (isObject (%this.waterPlane))
		{
			//update our waterplane
			%pos = getWords (groundPlane.getTransform (), 0, 2);
			%pos = VectorAdd (%pos, "0 0 " @ %other.var_WaterHeight);
			%this.waterPlane.setTransform (%pos @ " 0 0 1 0");
			%this.waterPlane.scrollSpeed = %other.var_WaterScrollX SPC %other.var_WaterScrollY;
			%this.waterPlane.color = getColorI (%other.var_WaterColor);
			%this.waterPlane.blend = getWord (%this.waterPlane.color, 3) < 255;
			
			//updateWaterFog() :
			%height = getWord (%this.WaterPlane.getTransform (), 2);
			%waterVis = 220 - getWord (%other.var_WaterColor, 3) * 200;
			%this.sky.fogVolume1 = %waterVis SPC -10 SPC %height;
		} else {
			//make a copy of the waterplane
			%name = %other.waterPlane.getName();
			%other.waterPlane.setName("Template");
			%this.waterPlane = new FxPlane(Copy : Template).getId();
			%this.waterPlane.setName("");
			%other.waterPlane.setName(%name);

			%height = getWord (%this.WaterPlane.getTransform (), 2);
			%waterVis = 220 - getWord (%other.var_WaterColor, 3) * 200;
			%this.sky.fogVolume1 = %waterVis SPC -10 SPC %height;
		}
	} else if(isObject(%this.waterPlane)) //delete our waterplane since other doesnt have one
		%this.waterPlane.delete();

	if(isObject(%other.waterZone))
	{
		if (isObject (%this.waterZone))
		{
			%pos = getWords (%this.WaterPlane.getTransform (), 0, 2);
			%pos = VectorSub (%pos, "0 0 100");
			%pos = VectorAdd (%pos, "0 0 0.5");
			%pos = VectorSub (%pos, "500000 -500000 0");
			%this.waterZone.setTransform (%pos @ " 0 0 1 0");
			%this.waterZone.appliedForce = %other.var_WaterScrollX * 414 SPC %other.var_WaterScrollY * -414 SPC 0;
			%this.waterZone.setWaterColor (getColorF (%other.var_UnderWaterColor));
		} else {
			%name = %other.waterZone.getName();
			%other.waterZone.setName("Template");
			%this.waterZone = new PhysicalZone(Copy : Template).getId();
			%this.waterZone.setName("");
			%other.waterZone.setName(%name);
		}
	} else if(isObject(%this.waterZone))
		%this.waterZone.delete();

	if(isObject(%other.rain))
	{
		%name = %other.rain.getName();
		%other.rain.setName("Template");
		%this.rain = new Precipitation(Copy : Template).getId();
		%this.rain.setName("");
		%other.rain.setName(%name);
	} else if(isObject(%this.rain))
		%this.rain.delete();
	

	%this.sky.renderBottomTexture = getWord (%this.groundPlane.color, 3) <= 0;
	%this.sky.noRenderBans = %this.sky.renderBottomTexture;
	
	//im just gonna ignore this
	//loadDayCycle ($EnvGuiServer::DayCycle[%other.var_DayCycleIdx]);
	if(isObject(%this.daycycle))
		%this.daycycle.delete();

	%name = %other.dayCycle.getName();
	%other.dayCycle.setName("Template");
	%this.dayCycle = new FxDayCycle(Copy : Template).getId();
	%this.dayCycle.setName("");
	%other.dayCycle.setName(%name);

	%this.DayCycle.setEnabled (%other.var_DayCycleEnabled);

	%this.sun.sendUpdate ();
	%this.sunLight.sendUpdate ();
	%this.sky.sendUpdate ();
	if(isObject(%this.waterPlane))
		%this.waterPlane.sendUpdate ();

	%this.groundPlane.sendUpdate ();
}