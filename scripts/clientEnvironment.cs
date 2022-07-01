//%other is a zone environment (template for the client zone)
function Environment::setClientEnv(%this, %other)
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

		if(GhostAlwaysSet.isMember(%this.sun))
			%this.sun.clearScopeAlways();

		%this.sun.setNetFlag(6, true);
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

		if(GhostAlwaysSet.isMember(%this.sunLight))
			%this.sunLight.clearScopeAlways();

		%this.sunLight.setNetFlag(6, true);
		%this.sunLight.scopeToClient(%this.client);
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

		if(GhostAlwaysSet.isMember(%this.sky))
			%this.sky.clearScopeAlways();

		%this.sky.setNetFlag(6, true);
		%this.sky.scopeToClient(%this.client);
	}

	if(isObject(%this.groundPlane))
	{
		%this.groundPlane.setTransform(%other.groundPlane.getTransform());
		%this.groundPlane.color = getColorI (%other.var_GroundColor);
		%this.groundPlane.blend = getWord (%other.groundPlane.color, 3) < 255;
		%this.groundPlane.scrollSpeed = %other.var_GroundScrollX SPC %other.var_GroundScrollY;
	} else {
		%name = %other.groundPlane.getName();
		%other.groundPlane.setName("Template");
		%this.groundPlane = new FxPlane(Copy : Template).getId();
		%this.groundPlane.setName("");
		%other.groundPlane.setName(%name);

		if(GhostAlwaysSet.isMember(%this.groundPlane))
			%this.groundPlane.clearScopeAlways();

		%this.groundPlane.setNetFlag(6, true);
		%this.groundPlane.scopeToClient(%this.client);
	}

	//does the other have a waterplane?
	if(isObject(%other.waterPlane))
	{
		if(isObject (%this.waterPlane))
		{
			//update our waterplane
			%this.waterPlane.setTransform (%other.waterPlane.getTransform());
			%this.waterPlane.scrollSpeed = %other.waterplane.scrollSpeed;
			%this.waterPlane.color = getColorI (%other.var_WaterColor);
			%this.waterPlane.blend = getWord (%this.waterPlane.color, 3) < 255;
			
			//updateWaterFog() :
			%this.sky.fogVolume1 = %other.sky.fogVolume1;
		} else {
			//make a copy of the waterplane
			%name = %other.waterPlane.getName();
			%other.waterPlane.setName("Template");
			%this.waterPlane = new FxPlane(Copy : Template).getId();
			%this.waterPlane.setName("");
			%other.waterPlane.setName(%name);

			if(GhostAlwaysSet.isMember(%this.waterPlane))
				%this.waterPlane.clearScopeAlways();

			%this.waterPlane.setNetFlag(6, true);
			%this.waterPlane.scopeToClient(%this.client);

			%this.sky.fogVolume1 = %other.sky.fogVolume1;
		}
	} else if(isObject(%this.waterPlane)) //delete our waterplane since other doesnt have one
		%this.waterPlane.delete();

	if(isObject(%other.waterZone))
	{
		if(isObject (%this.waterZone))
		{
			%this.waterZone.setTransform (%other.waterZone.getTransform());
			%this.waterZone.appliedForce = %other.waterZone.appliedForce;
			%this.waterZone.setWaterColor (getColorF (%other.var_UnderWaterColor));
		} else {
			%name = %other.waterZone.getName();
			%other.waterZone.setName("Template");
			%this.waterZone = new PhysicalZone(Copy : Template).getId();
			%this.waterZone.setName("");
			%other.waterZone.setName(%name);

			if(GhostAlwaysSet.isMember(%this.waterZone))
				%this.waterZone.clearScopeAlways();

			%this.waterZone.setNetFlag(6, true);
			%this.waterZone.scopeToClient(%this.client);
		}
	} else if(isObject(%this.waterZone))
		%this.waterZone.delete();

	%this.sky.renderBottomTexture = getWord (%this.groundPlane.color, 3) <= 0;
	%this.sky.noRenderBans = %this.sky.renderBottomTexture;
	
	if(!isObject(%this.dayCycle))
	{
		%name = %other.dayCycle.getName();
		%other.dayCycle.setName("Template");
		%this.dayCycle = new FxDayCycle(Copy : Template).getId();
		%this.dayCycle.setName("");
		%other.dayCycle.setName(%name);

		if(GhostAlwaysSet.isMember(%this.dayCycle))
			%this.dayCycle.clearScopeAlways();

		%this.dayCycle.setNetFlag(6, true);
		%this.dayCycle.scopeToClient(%this.client);
	}

	//TODO: im just gonna ignore this
	//loadDayCycle ($EnvGuiServer::DayCycle[%other.var_DayCycleIdx]);

	%this.DayCycle.setEnabled (%other.var_DayCycleEnabled);

	if(%this.var_SkyIdx != %other.var_SkyIdx)
		%this.setSkyBox(%other, %noUpdate = true);

	if(%this.var_GroundIdx != %other.var_GroundIdx)
		%this.setGround(%other, %noUpdate = true);

	if(%this.var_WaterIdx != %other.var_WaterIdx)
		%this.setWater(%other, %noUpdate = true);

	%this.sun.sendUpdate ();
	%this.sunLight.sendUpdate ();
	%this.sky.sendUpdate ();
	if(isObject(%this.waterPlane))
		%this.waterPlane.sendUpdate ();

	%this.groundPlane.sendUpdate ();
}

//this function is thrown around a lot
//parseEnvironmentFile (%filename);
//all it really does is assign abunch of environment variables based on an 'atmosphere' filepath

function Environment::setGround(%this, %other, %noUpdate)
{
	if(!isObject(%other))
		return;

	%thisGP = %this.groundPlane;
	%otherGP = %other.groundPlane;

	%thisGP.topTexture = %otherGP.TopTexture;
	%thisGP.bottomTexture = %otherGP.TopTexture;
	%thisGP.loopsPerUnit = %otherGP.LoopsPerUnit;
	%thisGP.scrollSpeed = %otherGP.ScrollSpeed;
	%thisGP.color = %otherGP.Color;
	%thisGP.blend = getWord (%thisGP.color, 3) < 255;
	%thisGP.colorMultiply = %otherGP.ColorMultiply;
	%thisGP.rayCastColor = %otherGP.RayCastColor;

	%this.Sky.renderBottomTexture = getWord (%thisGP.color, 3) <= 0;
	%this.Sky.noRenderBans = %this.Sky.renderBottomTexture;
	
	//gonna do a big update all at once instead
	if(%noUpdate)
		return;

	%this.Sky.sendUpdate ();
	%thisGP.sendUpdate ();
}

function Environment::setWater (%this, %other, %noUpdate)
{
	if(!isObject(%other.waterPlane))
	{
		%this.Sky.fogVolume1 = %other.sky.fogVolume1;
		if(!%noUpdate)
			Sky.sendUpdate ();

		if(isObject (%this.WaterPlane))
			%this.WaterPlane.delete();
		if(isObject (%this.WaterZone))
			%this.WaterZone.delete();

		return;
	}
	//parseEnvironmentFile (%filename);
	if(!isObject (%this.WaterPlane))
	{
		%name = %other.waterPlane.getName();
		%other.waterPlane.setName("Template");
		%this.waterPlane = new FxPlane(Copy : Template).getId();
		%this.waterPlane.setName("");
		%other.waterPlane.setName(%name);

		if(GhostAlwaysSet.isMember(%this.waterPlane))
			%this.waterPlane.clearScopeAlways();

		%this.waterPlane.setNetFlag(6, true);
		%this.waterPlane.scopeToClient(%this.client);

		%createdWaterPlane = true;
	}
	if(!isObject (WaterZone))
	{
		%name = %other.waterZone.getName();
		%other.waterZone.setName("Template");
		%this.waterZone = new PhysicalZone(Copy : Template).getId();
		%this.waterZone.setName("");
		%other.waterZone.setName(%name);

		if(GhostAlwaysSet.isMember(%this.waterZone))
			%this.waterZone.clearScopeAlways();

		%this.waterZone.setNetFlag(6, true);
		%this.waterZone.scopeToClient(%this.client);

		if(%createdWaterPlane)
			return;

		%createdWaterZone = true;
	}

	%thisWP = %this.waterPlane;
	%otherWP = %other.waterPlane;

	if(!%createdWaterPlane)
	{
		%thisWP.topTexture = $Water::TopTexture;
		%thisWP.bottomTexture = $Water::BottomTexture;
		%thisWP.loopsPerUnit = $Water::LoopsPerUnit;
		%thisWP.scrollSpeed = $Water::ScrollSpeed;
		%thisWP.color = $Water::Color;
		%thisWP.colorMultiply = $Water::ColorMultiply;
		%thisWP.blend = getWord (%thisWP.color, 3) < 255;

		%thisWP.setTransform (%otherWP.getTransform());

		if(!%noUpdate)
			%thisWP.sendUpdate ();
	}
	
	%this.Sky.fogVolume1 = %other.sky.fogVolume1;
	if(!%noUpdate)
		%this.Sky.sendUpdate ();

	%this.WaterZone.appliedForce = %other.waterZone.appliedForce;
	%this.WaterZone.setTransform (%other.waterZone.getTransform());
}

function Environment::setSkyBox (%this, %other, %noUpdate)
{
	%thisSky = %this.sky;
	%otherSky = %other.sky;

	//this doesnt really cause that much lag
	%thisSky.materialList = %otherSky.materialList;
	%thisSky.visibleDistance = %otherSky.visibleDistance;
	%thisSky.fogDistance = %otherSky.fogDistance;
	%thisSky.fogColor = %otherSky.fogColor;
	%thisSky.noRenderBans = %otherSky.noRenderBans;
	%thisSky.skyColor = %otherSky.skyColor;
	%thisSky.cloudHeight[0] = %otherSky.cloudHeight[0];
	%thisSky.cloudHeight[1] = %otherSky.cloudHeight[1];
	%thisSky.cloudHeight[2] = %otherSky.cloudHeight[2];
	%thisSky.cloudSpeed[0] = %otherSky.cloudSpeed[0];
	%thisSky.cloudSpeed[1] = %otherSky.cloudSpeed[1];
	%thisSky.cloudSpeed[2] = %otherSky.cloudSpeed[2];
	%thisSky.windVelocity = %otherSky.windVelocity;
	%thisSky.windEffectPrecipitation = %otherSky.windEffectPrecipitation;

	%thisSun = %this.sun;
	%otherSun = %other.sun;

	%thisSun.azimuth = %otherSun.azimuth;
	%thisSun.elevation = %otherSun.elevation;
	%thisSun.color = %otherSun.color;
	%thisSun.ambient = %otherSun.ambient;
	%thisSun.shadowColor = %otherSun.shadowColor;

	%this.SunLight.setFlareBitmaps (%other.sunLight.removeFlareBitmap, %other.sunLight.localFlareBitmap);
	%this.SunLight.FlareSize = %other.SunLight.FlareSize;
	%this.SunLight.color = %other.SunLight.color;

	if(isObject (%this.Rain))
		%this.Rain.delete ();

	//dont like how rain is bound to skyboxes
	if(isObject(%other.rain))
	{
		%name = %other.rain.getName();
		%other.rain.setName("Template");
		%this.rain = new Precipitation(Copy : Template).getId();
		%this.rain.setName("");
		%other.rain.setName(%name);

		if(GhostAlwaysSet.isMember(%this.rain))
			%this.rain.clearScopeAlways();

		%this.rain.setNetFlag(6, true);
		%this.rain.scopeToClient(%this.client);
	}

	if(!%noUpdate)
	{
		%thisSky.sendUpdate ();
		%thisSun.sendUpdate ();
	}

	%this.loadDayCycle(%other, %noUpdate);

	%this.DayCycle.setDayLength (%other.var_DayLength);
	%this.DayCycle.setEnabled (%other.var_DayCycleEnabled);
}

function Environment::loadDayCycle (%this, %other, %noUpdate)
{
	%thisDC = %this.dayCycle;
	%otherDC = %other.dayCycle;
	for(%i = 0; %i < 20; %i++)
	{
		%thisDC.targetFraction[%i] = %otherDC.targetFraction[%i];
		%thisDC.targetDirectColor[%i] = %otherDC.targetDirectColor[%i];
		%thisDC.targetAmbientColor[%i] = %otherDC.targetAmbientColor[%i];
		%thisDC.targetSkyColor[%i] = %otherDC.targetSkyColor[%i];
		%thisDC.targetFogColor[%i] = %otherDC.targetFogColor[%i];
		%thisDC.targetShadowColor[%i] = %otherDC.targetShadowColor[%i];
		%thisDC.targetSunFlareColor[%i] = %otherDC.targetSunFlareColor[%i];
		%thisDC.targetUseDefaultVector[%i] = %otherDC.targetUseDefaultVector[%i];
	}

	if(!%noUpdate)
		%thisDC.sendUpdate ();
}


//returns if you should clone the object or not
function Environment::SetVar(%this, %varName, %value, %other)
{
	switch$(%varName)
	{
		case "SimpleMode":
			return "ALL";

		case "SkyIdx":
			if(%this.var_SkyIdx !$= %value)
				%this.setSkyBox(%other);

		case "WaterIdx":
			if(%this.var_WaterIdx !$= %value)
				%this.setWater(%other); //water is different lets just copy the variables from %other

		case "GroundIdx":
			if(%this.var_GroundIdx !$= %value)
				%this.setGround(%other); //ground is different lets just copy the variables from %other

		case "DayCycleIdx":
			if(%this.var_DayCycleIdx !$= %value)
				%this.loadDayCycle (%other);

		case "DayOffset":
			%value = mClampF (%value, 0, 1);
			%this.DayCycle.setDayOffset (%value);

		case "DayLength":
			if(%this.var_DayLength !$= %value)
			{
				%this.var_DayLength = mClamp (%value, 0, 86400);
				DayCycle.setDayLength (%this.var_DayLength);
			}

		case "DayCycleEnabled":
			if(%this.var_DayCycleEnabled !$= %value)
			{
				%this.var_DayCycleEnabled = mClamp (%value, 0, 1);
				DayCycle.setEnabled (%this.var_DayCycleEnabled);
			}

		case "SunFlareTopIdx":
			if(%this.var_SunFlareTopIdx !$= %value)
			{
				%this.var_SunFlareTopIdx = mClamp (%value, 0, $EnvGuiServer::SunFlareCount);
				%top = $EnvGuiServer::SunFlare[%this.var_SunFlareTopIdx];
				%bottom = $EnvGuiServer::SunFlare[%this.var_SunFlareBottomIdx];
				%this.SunLight.setFlareBitmaps (%top, %bottom);
			}
		case "SunFlareBottomIdx":
			if(%this.var_SunFlareBottomIdx !$= %value)
			{
				%this.var_SunFlareBottomIdx = mClamp (%value, 0, $EnvGuiServer::SunFlareCount);
				%top = $EnvGuiServer::SunFlare[%this.var_SunFlareTopIdx];
				%bottom = $EnvGuiServer::SunFlare[%this.var_SunFlareBottomIdx];
				%this.SunLight.setFlareBitmaps (%top, %bottom);
			}
		case "SunAzimuth":
			if(%this.var_SunAzimuth !$= %value)
			{
				%this.var_SunAzimuth = mClampF (%value, 0, 360);
				%this.Sun.azimuth = %this.var_SunAzimuth;
				%this.Sun.sendUpdate ();
			}
		case "SunElevation":
			if(%this.var_SunElevation !$= %value)
			{
				%this.var_SunElevation = mClampF (%value, -10, 190);
				%this.Sun.elevation = %this.var_SunElevation;
				%this.Sun.sendUpdate ();
			}
		case "DirectLightColor":
			if(%this.var_DirectLightColor !$= %value)
			{
				%this.var_DirectLightColor = getColorF (%value);
				%this.Sun.color = %this.var_DirectLightColor;
				%this.Sun.sendUpdate ();
			}
		case "AmbientLightColor":
			if(%this.var_AmbientLightColor !$= %value)
			{
				%this.var_AmbientLightColor = getColorF (%value);
				%this.Sun.ambient = %this.var_AmbientLightColor;
				%this.Sun.sendUpdate ();
			}
		case "ShadowColor":
			if(%this.var_ShadowColor !$= %value)
			{
				%this.var_ShadowColor = getColorF (%value);
				%this.Sun.shadowColor = %this.var_ShadowColor;
				%this.Sun.sendUpdate ();
			}
		case "SunFlareColor":
			if(%this.var_SunFlareColor !$= %value)
			{
				%this.var_SunFlareColor = getColorF (%value);
				%this.SunLight.color = %this.var_SunFlareColor;
				%this.SunLight.sendUpdate ();
			}
		case "SunFlareSize":
			if(%this.var_SunFlareSize !$= %value)
			{
				%this.var_SunFlareSize = mClampF (%value, 0, 10);
				%this.SunLight.FlareSize = %this.var_SunFlareSize;
				%this.SunLight.sendUpdate ();
			}
		case "SunFlareIdx": //what does this even do?
			if(%this.var_SunFlareIdx !$= %value)
			{
				%this.var_SunFlareIdx = mClamp (%value, 0, %this.var_SunFlareCount);
			}
		case "VisibleDistance":
			if(%this.var_VisibleDistance !$= %value)
			{
				%this.var_VisibleDistance = mClampF (%value, 0, 1000);
				%this.Sky.visibleDistance = %this.var_VisibleDistance;
				%this.Sky.sendUpdate ();
			}
		case "FogDistance":
			if(%this.var_FogDistance !$= %value)
			{
				%this.var_FogDistance = mClampF (%value, 0, 1000);
				%this.Sky.fogDistance = %this.var_FogDistance;
				%this.Sky.sendUpdate ();
			}
		case "FogHeight": //i dont think this does anything either
			if(%this.var_FogHeight !$= %value)
			{
				%this.var_FogHeight = mClampF (%value, 0, 1000);
			}
		case "FogColor":
			if(%this.var_FogColor !$= %value)
			{
				%this.var_FogColor = getColorF (%value);
				%this.Sky.fogColor = %this.var_FogColor;
				%this.Sky.sendUpdate ();
			}
		case "WaterColor":
			if(%this.var_WaterColor !$= %value)
			{
				%this.var_WaterColor = getColorF (%value);
				if(isObject (%this.WaterPlane))
				{
					%this.WaterPlane.color = getColorI (%this.var_WaterColor);
					%this.WaterPlane.blend = getWord (%this.WaterPlane.color, 3) < 255;
					%this.WaterPlane.sendUpdate ();
					
					%waterVis = 220 - getWord ($EnvGuiServer::WaterColor, 3) * 200;
					%this.Sky.fogVolume1 = %waterVis SPC -10 SPC %height;
					%this.Sky.sendUpdate ();
				}
			}
		case "WaterHeight":
			if(%this.var_WaterHeight !$= %value)
			{
				%this.var_WaterHeight = mClampF (%value, 0, 100);
				if(isObject (%this.WaterPlane))
				{
					%pos = getWords (%this.groundPlane.getTransform (), 0, 2);
					%pos = VectorAdd (%pos, "0 0 " @ %this.var_WaterHeight);
					%this.WaterPlane.setTransform (%pos @ " 0 0 1 0");
					%this.WaterPlane.sendUpdate ();
					%height = getWord (%this.WaterPlane.getTransform (), 2);
					%waterVis = 220 - getWord ($EnvGuiServer::WaterColor, 3) * 200;
					%this.Sky.fogVolume1 = %waterVis SPC -10 SPC %height;
					%this.Sky.sendUpdate ();
				}
				if(isObject (%this.WaterZone))
				{
					%pos = getWords (%this.WaterPlane.getTransform (), 0, 2);
					%pos = VectorSub (%pos, "0 0 100");
					%pos = VectorAdd (%pos, "0 0 0.5");
					%pos = VectorSub (%pos, "500000 -500000 0");
					%this.WaterZone.setTransform (%pos @ " 0 0 1 0");
				}
			}
		case "UnderWaterColor":
			if(%this.var_UnderWaterColor !$= %value)
			{
				%this.var_UnderWaterColor = getColorF (%value);
				if(isObject (%this.WaterZone))
				{
					%this.WaterZone.setWaterColor (%this.var_UnderWaterColor);
				}
			}
		case "SkyColor":
			if(%this.var_SkyColor !$= %value)
			{
				%this.var_SkyColor = getColorF (%value);
				%this.Sky.skyColor = getColorF (%this.var_SkyColor);
				%this.Sky.sendUpdate ();
			}
		case "WaterScrollX":
			if(%this.var_WaterScrollX !$= %value)
			{
				%this.var_WaterScrollX = %value;
				%this.var_WaterScrollX = mClampF (%this.var_WaterScrollX, -10, 10);
				%this.var_WaterScrollY = mClampF (%this.var_WaterScrollY, -10, 10);
				if(isObject (%this.WaterPlane))
				{
					%this.WaterPlane.scrollSpeed = %this.var_WaterScrollX SPC %this.var_WaterScrollY;
					%this.WaterPlane.sendUpdate ();
				}
				if(isObject (%this.WaterZone))
				{
					%this.WaterZone.appliedForce = %this.var_WaterScrollX * 414 SPC %this.var_WaterScrollY * -414 SPC 0;
					%this.WaterZone.sendUpdate ();
				}
			}
		case "WaterScrollY":
			if(%this.var_WaterScrollX !$= %value)
			{
				%this.var_WaterScrollY = %value;
				%this.var_WaterScrollX = mClampF (%this.var_WaterScrollX, -10, 10);
				%this.var_WaterScrollY = mClampF (%this.var_WaterScrollY, -10, 10);
				if(isObject (%this.WaterPlane))
				{
					%this.WaterPlane.scrollSpeed = %this.var_WaterScrollX SPC %this.var_WaterScrollY;
					%this.WaterPlane.sendUpdate ();
				}
				if(isObject (%this.WaterZone))
				{
					%this.WaterZone.appliedForce = %this.var_WaterScrollX * 414 SPC %this.var_WaterScrollY * -414 SPC 0;
					%this.WaterZone.sendUpdate ();
				}
			}
		case "GroundColor":
			if(%this.var_GroundColor !$= %value)
			{
				%this.var_GroundColor = getColorF (%value);
				if(isObject (%this.groundPlane))
				{
					%this.groundPlane.color = getColorI (%this.var_GroundColor);
					%this.groundPlane.blend = getWord (%this.groundPlane.color, 3) < 255;
					%this.groundPlane.sendUpdate ();
					%this.Sky.renderBottomTexture = getWord (%this.groundPlane.color, 3) <= 0;
					%this.Sky.noRenderBans = %this.Sky.renderBottomTexture;
					%this.Sky.sendUpdate ();
				}
			}
		case "GroundScrollX":
			if(%this.var_GroundScrollX !$= %value)
			{
				%this.var_GroundScrollX = %value;
				%this.var_GroundScrollX = mClampF (%this.var_GroundScrollX, -10, 10);
				%this.var_GroundScrollY = mClampF (%this.var_GroundScrollY, -10, 10);
				%this.groundPlane.scrollSpeed = %this.var_GroundScrollX SPC %this.var_GroundScrollY;
				%this.groundPlane.sendUpdate ();
			}
		case "GroundScrollY":
			if(%this.var_GroundScrollY !$= %value)
			{
				%this.var_GroundScrollY = %value;
				%this.var_GroundScrollX = mClampF (%this.var_GroundScrollX, -10, 10);
				%this.var_GroundScrollY = mClampF (%this.var_GroundScrollY, -10, 10);
				%this.groundPlane.scrollSpeed = %this.var_GroundScrollX SPC %this.var_GroundScrollY;
				%this.groundPlane.sendUpdate ();
			}
		case "VignetteMultiply":
			if(%this.var_VignetteMultiply !$= %value)
			{
				%this.var_VignetteMultiply = mClamp (%value, 0, 1);
				EnvGuiServer::SendVignetteAll ();
			}
		case "VignetteColor":
			if(%this.var_VignetteColor !$= %value)
			{
				%this.var_VignetteColor = getColorF (%value);
				EnvGuiServer::SendVignetteAll ();
			}
	}
	return "";
}
