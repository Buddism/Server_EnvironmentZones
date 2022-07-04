function Environment::initTransition(%this, %other)
{
	//short vars:
	// LE - LERP_ENABLED
	// LS - LERP_START
	//overlapping var names will be differentiatied by short name of the object
	%this.lastTransitionValue = 0;

	%thisSun = %this.sun;
	%otherSun = %other.sun;

	%thisSky = %this.sky;
	%otherSky = %other.sky;

	%thisWP = %this.waterPlane;
	%otherWP = %other.waterPlane;

	%thisWZ = %this.waterZone;
	%otherWZ = %other.waterZone;

	%thisGP = %this.groundPlane;
	%otherGP = %other.groundPlane;

	//sun
	if(%thisSun.azimuth					!$=	%otherSun.azimuth				) {	%this.LES_azimuth	 	 = true;	%this.LSS_azimuth	 	 =	%thisSun.azimuth;				%sunLerps++; } else %this.LES_azimuth	 	 = false;
	if(%thisSun.elevation				!$=	%otherSun.elevation				) {	%this.LES_elevation	 	 = true;	%this.LSS_elevation	 	 =	%thisSun.elevation;				%sunLerps++; } else %this.LES_elevation	 	 = false;
	if(%thisSun.color					!$=	%otherSun.color					) {	%this.LES_color		 	 = true;	%this.LSS_color		 	 =	%thisSun.color;					%sunLerps++; } else %this.LES_color		 	 = false;
	if(%thisSun.ambient					!$=	%otherSun.ambient				) {	%this.LES_ambient	 	 = true;	%this.LSS_ambient	 	 =	%thisSun.ambient;				%sunLerps++; } else %this.LES_ambient	 	 = false;
	if(%thisSun.shadowColor				!$=	%otherSun.shadowColor			) {	%this.LES_shadowColor 	 = true;	%this.LSS_shadowColor 	 =	%thisSun.shadowColor;			%sunLerps++; } else %this.LES_shadowColor 	 = false;

	//sunlight
	if(%this.sunLight.FlareSize			!$= %other.sunLight.FlareSize		) { %this.LESL_FlareSize	 = true;	%this.LSSL_FlareSize	 =	%this.sunLight.FlareSize;		%sunLight++; } else %this.LESL_FlareSize	 = false;
	if(%this.sunLight.color				!$= %other.sunLight.color			) { %this.LESL_color	 	 = true;	%this.LSSL_color	 	 =	%this.sunLight.color; 			%sunLight++; } else %this.LESL_color	 	 = false;

	//sky
	if(%thisSky.visibleDistance			!$=	%otherSky.visibleDistance		) {	%this.LES_visibleDistance = true;	%this.LSS_visibleDistance =	%thisSky.visibleDistance;		%skyLerps++; } else %this.LES_visibleDistance = false;
	if(%thisSky.fogDistance				!$=	%otherSky.fogDistance			) {	%this.LES_fogDistance	 = true;	%this.LSS_fogDistance	 =	%thisSky.fogDistance;			%skyLerps++; } else %this.LES_fogDistance	 = false;
	if(%thisSky.fogColor				!$=	%otherSky.fogColor				) {	%this.LES_fogColor		 = true;	%this.LSS_fogColor		 =	%thisSky.fogColor;				%skyLerps++; } else %this.LES_fogColor		 = false;
	if(%thisSky.skyColor				!$=	%otherSky.skyColor				) {	%this.LES_skyColor		 = true;	%this.LSS_skyColor		 =	%thisSky.skyColor;				%skyLerps++; } else %this.LES_skyColor		 = false;
	if(%thisSky.fogVolume1				!$=	%otherSky.fogVolume1			) {	%this.LES_fogVolume1	 = true;	%this.LSS_fogVolume1	 =	%thisSky.fogVolume1;			%skyLerps++; } else %this.LES_fogVolume1		 = false;
	if(%thisSky.windVelocity 			!$= %otherSky.windVelocity			) {	%this.LES_windVelocity	 = true;	%this.LSS_windVelocity	 =	%thisSky.windVelocity;			%skyLerps++; } else %this.LES_windVelocity	 = false;

	if( (%thisSky.windEffectPrecipitation || %otherSky.windEffectPrecipitation) )
		%thisSky.windEffectPrecipitation = true;

	//TODO: OBJECT EXISTANCE STUFF
	if(isObject(%thisWP) && isObject(%otherWP))
	{
		if(%thisWP.getTransform() 		!$= %otherWP.getTransform()			) { %this.LEW_transform		 = true;	%this.LSW_transform		 = %thisWP.getTransform();			%WPLerps++;	 } else %this.LEW_transform		 = false;
		if(%thisWP.color 				!$= %otherWP.color					) { %this.LEW_color			 = true;	%this.LSW_color			 = %thisWP.color;					%WPLerps++;	 } else %this.LEW_color			 = false;
		if(%thisWP.scrollSpeed 			!$= %otherWP.scrollSpeed			) { %this.LEW_scrollSpeed	 = true;	%this.LSW_scrollSpeed	 = %thisWP.scrollSpeed;				%WPLerps++;	 } else %this.LEW_scrollSpeed	 = false;
		if(%thisWP.loopsPerUnit 		!$= %otherWP.loopsPerUnit			) { %this.LEW_loopsPerUnit	 = true;	%this.LSW_loopsPerUnit	 = %thisWP.loopsPerUnit;			%WPLerps++;	 } else %this.LEW_loopsPerUnit	 = false;
		if(%thisWP.rayCastColor 		!$= %otherWP.rayCastColor			) { %this.LEW_rayCastColor	 = true;	%this.LSW_rayCastColor	 = %thisWP.rayCastColor;			%WPLerps++;	 } else %this.LEW_rayCastColor	 = false;
	} else {
		if(!isObject(%thisWP) && !isObject(%otherWP))
		{
			%this.LEW_transform		 = false;
			%this.LEW_color			 = false;
			%this.LEW_scrollSpeed	 = false;
			%this.LEW_loopsPerUnit	 = false;
			%this.LEW_rayCastColor	 = false;
		} else {
			if(isObject(%thisWP))
			{
				//need to shrink thisWP to nothing
				%this.LEW_transform		 = true;
				%this.LSW_transform		 = %otherWP.getPosition();
				%WPLerps = 1;

				%this.LEW_color			 = false;
				%this.LEW_scrollSpeed	 = false;
				%this.LEW_loopsPerUnit	 = false;
				%this.LEW_rayCastColor	 = false;

			} else { //%otherWP exists insteade of %thisWP
				//need to grow thisWP (which doesnt exist yet) to otherWP
				%this.LEW_transform		 = true;
				%WPLerps = 1;

				%this.LEW_color			 = false;
				%this.LEW_scrollSpeed	 = false;
				%this.LEW_loopsPerUnit	 = false;
				%this.LEW_rayCastColor	 = false;

				//create our water plane
				%this.copyWaterPlaneFrom(%otherWP);
				%thisWP = %this.waterPlane;

				if(isObject(%other.zone))
				{
					//height of the other WaterPlane
					%height = getWord(%otherWP.getTransform(), 2) - 0.05;
					%zoneZ1 = getWord(%other.zone.point1, 2); //the lowest corner

					//if the height is inside the zone
					if(%height > %zoneZ1)
						%startOfTransition = %zoneZ1;
					else
						%startOfTransition = "0 0 0";
				} else %startOfTransition = "0 0 0";
				//%bottomOfEnvZone

				//might be a bad idea to do it from the bottom of the env zone
				%thisWP.setTransform(%startOfTransition);
				%thisWP.LSW_transform = %startOfTransition;
			}
		}
	}

	if(isObject(%thisWZ) && isObject(%otherWZ))
	{
		if(%this.waterZone.waterColor	!$= %other.waterZone.waterColor		) { %this.LEZ_waterColor	 = true;	%this.LSZ_waterColor	= %this.waterZone.waterColor;		%WZLerps++;	 } else %this.LEZ_waterColor	 = false;
	} else {
		%this.LEZ_waterColor	= false;
		
		if(isObject(%thisWZ))
			%this.waterZone.delete();
		else
			%this.copyWaterZoneFrom(%other);
	}

	//not sure if cloudHeight is used but im adding
	if(%thisSky.cloudHeight[0]			!$=	%otherSky.cloudHeight[0]		) {	%this.LE_cloudHeight[0]	 = true;	%this.LS_cloudHeight[0]	= %thisSky.cloudHeight[0];			%cloudLerps++; } else %this.LE_cloudHeight[0]	 = false;
	if(%thisSky.cloudHeight[1]			!$=	%otherSky.cloudHeight[1]		) {	%this.LE_cloudHeight[1]	 = true;	%this.LS_cloudHeight[1]	= %thisSky.cloudHeight[1];			%cloudLerps++; } else %this.LE_cloudHeight[1]	 = false;
	if(%thisSky.cloudHeight[2]			!$=	%otherSky.cloudHeight[2]		) {	%this.LE_cloudHeight[2]	 = true;	%this.LS_cloudHeight[2]	= %thisSky.cloudHeight[2];			%cloudLerps++; } else %this.LE_cloudHeight[2]	 = false;
	if(%thisSky.cloudSpeed[0]			!$=	%otherSky.cloudSpeed[0]			) {	%this.LE_cloudSpeed[0]	 = true;	%this.LS_cloudSpeed[0]	= %thisSky.cloudSpeed[0];			%cloudLerps++; } else %this.LE_cloudSpeed[0]	 = false;
	if(%thisSky.cloudSpeed[1]			!$=	%otherSky.cloudSpeed[1]			) {	%this.LE_cloudSpeed[1]	 = true;	%this.LS_cloudSpeed[1]	= %thisSky.cloudSpeed[1];			%cloudLerps++; } else %this.LE_cloudSpeed[1]	 = false;
	if(%thisSky.cloudSpeed[2]			!$=	%otherSky.cloudSpeed[2]			) {	%this.LE_cloudSpeed[2]	 = true;	%this.LS_cloudSpeed[2]	= %thisSky.cloudSpeed[2];			%cloudLerps++; } else %this.LE_cloudSpeed[2]	 = false;

	%this.skyLerps = %skyLerps;
	%this.sunLerps = %sunLerps;
	%this.sunlightLerps = %sunLight;
	%this.WaterZoneLerps = %WZLerps;
	%this.WaterPlaneLerps = %WPLerps;
	%this.cloudLerps = %cloudLerps;

	talk("skyLerps: " @ %this.skyLerps);
	talk("sunLerps: " @ %this.sunLerps);
	talk("sunlightLerps: " @ %this.sunlightLerps);
	talk("WaterZoneLerps: " @ %this.WaterZoneLerps);
	talk("WaterPlaneLerps: " @ %this.WaterPlaneLerps);
	talk("cloudLerps: " @ %this.cloudLerps);

	%other.real_vignetteColor = (%other.var_SimpleMode ? %other.simple_VignetteColor : %other.var_VignetteColor);
	%other.real_vignetteMultiply = (%other.var_SimpleMode ? %other.simple_VignetteMultiply : %other.var_VignetteMultiply);
	%this.LE_Vignette = (%other.real_vignetteColor !$= %this.real_vignetteColor);

	%this.LSG_opacity = getWord(%thisGP.color, 3);
	%thisGP.blend = true;
	%thisGP.sendUpdate();
	if(isObject(%this.transitionGroundPlane))
		%this.transitionGroundPlane.clearScopeToClient(%this.client);
		
	%otherGP.scopeToClient(%this.client);
	%this.transitionGroundPlane = %otherGP;
}

function Environment::TimeTransition(%this, %other, %time, %start)
{
	if(!isObject(%other))
		return;

	if(%start == 0)
	{
		%start = getSimTime();
		%this.initTransition(%other);
		cancel(%this.transitionSchedule);
	}

	%lerp = (getSimTime() - %start) / %time;
	%this.transitionEnvironment(%other, %lerp);

	%this.client.bottomPrint(%lerp, 1, 1);
	if(%lerp < 1)
		%this.transitionSchedule = %this.schedule(31, TimeTransition, %other, %time, %start);
}

function Environment::transitionEnvironment(%this, %other, %lerp)
{
	if(!isObject(%other))
		return;

	%lerp = mClampF(%lerp, 0, 1);
	if(%lerp >= 0.5 && %this.lastTransitionValue < 0.5)
		%this.lerpPassedMidpoint(%other, %lerp);
	else if(%lerp >= 1.0 && %this.lastTransitionValue < 1)
	{
		%this.transitionEnvironmentFinal(%other);
		%this.setClientEnv(%other);
		return;
	}

	
	%thisSun = %this.sun;
	%otherSun = %other.sun;
	%thisSky = %this.sky;
	%otherSky = %other.sky;
	%thisWP = %this.waterPlane;
	%otherWP = %other.waterPlane;
	%thisWZ = %this.waterZone;
	%otherWZ = %other.waterZone;

	//vignette
	if(%this.LE_Vignette)
	{
		%multiply = %lerp < 0.5 ? %this.real_vignetteMultiply : %other.real_vignetteMultiply;
		%vignetteLerp = EZ_Lerp4f(%this.real_vignetteColor, %other.real_vignetteColor, %lerp);
		commandToClient(%this.client, 'setVignette', %multiply, %vignetteLerp);

		//update these vars incase we cancel a transition and change to a different environment
		%this.real_vignetteColor = %vignetteLerp;
		%this.real_vignetteMultiply = %multiply;
	}

	if(%this.skyLerps > 0) //we have a difference at all in sky variables
	{
		//sky
		if(%this.LES_visibleDistance) { %thisSky.visibleDistance = EZ_Lerp1f(%this.LSS_visibleDistance	, %otherSky.visibleDistance , %lerp); }
		if(%this.LES_fogDistance	) { %thisSky.fogDistance	 = EZ_Lerp1f(%this.LSS_fogDistance		, %otherSky.fogDistance	 	, %lerp); }
		if(%this.LES_fogColor		) { %thisSky.fogColor		 = EZ_Lerp4f(%this.LSS_fogColor			, %otherSky.fogColor		, %lerp); }
		if(%this.LES_skyColor		) { %thisSky.skyColor		 = EZ_Lerp4f(%this.LSS_skyColor			, %otherSky.skyColor		, %lerp); }
		if(%this.LES_fogVolume1		) { %thisSky.fogVolume1		 = EZ_Lerp3f(%this.LSS_fogVolume1		, %otherSky.fogVolume1		, %lerp); }
		if(%this.LES_windVelocity	) { %thisSky.windVelocity	 = EZ_Lerp3f(%this.LSS_windVelocity		, %otherSky.windVelocity	, %lerp); }

		//need to do this because clouds and i dont want to double update
		%sendSkyUpdate = true;
	}

	//sun
	if(%this.sunLerps > 0)
	{
		if(%this.LES_azimuth	) { %thisSun.azimuth	 = EZ_Lerp1f(%this.LSS_azimuth	   , %otherSun.azimuth	   , %lerp); }
		if(%this.LES_elevation	) { %thisSun.elevation	 = EZ_Lerp1f(%this.LSS_elevation   , %otherSun.elevation   , %lerp); }
		if(%this.LES_color		) { %thisSun.color		 = EZ_Lerp4f(%this.LSS_color	   , %otherSun.color	   , %lerp); }
		if(%this.LES_ambient	) { %thisSun.ambient	 = EZ_Lerp4f(%this.LSS_ambient	   , %otherSun.ambient	   , %lerp); }
		if(%this.LES_shadowColor) { %thisSun.shadowColor = EZ_Lerp4f(%this.LSS_shadowColor , %otherSun.shadowColor , %lerp); }

		%thisSun.sendUpdate();
	}


	//sunlight
	if(%this.sunlightLerps > 0)
	{
		if(%this.LESL_FlareSize	) { %this.SunLight.FlareSize = EZ_Lerp1f(%this.LSSL_FlareSize, %other.SunLight.FlareSize,	%lerp); }
		if(%this.LESL_color	 	) { %this.SunLight.color	 = EZ_Lerp4f(%this.LSSL_color	 , %other.SunLight.color	  ,	%lerp); }

		%this.sunlight.sendUpdate();
	}

	

	//waterplane
	if(%this.WaterPlaneLerps > 0)
	{
		if(%this.LEW_transform		) { %thisWP.setTransform  (EZ_Lerp3f(%this.LSW_transform	, %otherWP.getTransform(), %lerp)); }
		if(%this.LEW_color			) { %thisWP.color 		 = EZ_Lerp4f(%this.LSW_color		, %otherWP.color		 , %lerp ); }
		if(%this.LEW_scrollSpeed	) { %thisWP.scrollSpeed  = EZ_Lerp1f(%this.LSW_scrollSpeed	, %otherWP.scrollSpeed	 , %lerp ); }
		if(%this.LEW_loopsPerUnit	) { %thisWP.loopsPerUnit = EZ_Lerp2f(%this.LSW_loopsPerUnit	, %otherWP.loopsPerUnit	 , %lerp ); }
		if(%this.LEW_rayCastColor	) { %thisWP.rayCastColor = EZ_Lerp1f(%this.LSW_rayCastColor	, %otherWP.rayCastColor	 , %lerp ); }

		%thisWP.blend = getWord (%thisWP.color, 3) < 255;

		%thisWP.sendUpdate();
	}

	//waterzone
	if(%this.waterZoneLerps > 0)
	{
		if(%this.LEZ_waterColor		) { %thisWZ.setWaterColor(EZ_Lerp4f(%this.LSZ_waterColor	, %other.var_UnderWaterColor, %lerp)); }
	}

	//cloud
	if(%this.cloudLerps > 0)
	{
		if(%this.LE_cloudHeight[0]) { %thisSky.cloudHeight[0] = EZ_Lerp1f(%this.LS_cloudHeight[0], %otherSky.cloudHeight[0], %lerp); }
		if(%this.LE_cloudHeight[1]) { %thisSky.cloudHeight[1] = EZ_Lerp1f(%this.LS_cloudHeight[1], %otherSky.cloudHeight[1], %lerp); }
		if(%this.LE_cloudHeight[2]) { %thisSky.cloudHeight[2] = EZ_Lerp1f(%this.LS_cloudHeight[2], %otherSky.cloudHeight[2], %lerp); }
		if(%this.LE_cloudSpeed[0] ) { %thisSky.cloudSpeed[0]  = EZ_Lerp1f(%this.LS_cloudSpeed[0] , %otherSky.cloudSpeed[0] , %lerp); }
		if(%this.LE_cloudSpeed[1] ) { %thisSky.cloudSpeed[1]  = EZ_Lerp1f(%this.LS_cloudSpeed[1] , %otherSky.cloudSpeed[1] , %lerp); }
		if(%this.LE_cloudSpeed[2] ) { %thisSky.cloudSpeed[2]  = EZ_Lerp1f(%this.LS_cloudSpeed[2] , %otherSky.cloudSpeed[2] , %lerp); }

		%sendSkyUpdate = true;
	}

	if(%sendSkyUpdate)
		%thisSky.sendUpdate();
	
	%thisGP = %this.groundPlane;
	%otherGP = %other.groundPlane;

	//assuming .blend is already set
	%thisGP.color = setWord(%thisGP.color, 3, EZ_Lerp1f(%this.LSG_opacity, 0, %lerp));

	%thisGP.sendUpdate();
	%this.lastTransitionValue = %lerp;
}


function Environment::lerpPassedMidpoint(%this, %other, %lerp)
{
	//do texture stuff & non-lerpable
	%this.waterPlane.colorMultiply = %other.waterPlane.colorMultiply;
}

function Environment::transitionEnvironmentFinal(%this, %other)
{
	if(isObject(%this.transitionGroundPlane))
		%this.transitionGroundPlane.clearScopeToClient(%this.client);

	%this.transitionGroundPlane = -1;
}

function EZ_Lerp4f(%init, %end, %t)
{
	//init4
	%i = getWord(%init, 3);
	return vectorAdd(%init, vectorScale(vectorSub(%end, %init), %t)) SPC (%i + (getWord(%end, 3) - %i) * %t);
}

function EZ_Lerp3f(%init, %end, %t)
{
	return vectorAdd(%init, vectorScale(vectorSub(%end, %init), %t));
}

function EZ_Lerp2f(%init, %end, %t)
{
	//dont feel like benchmarking this
	return getWords(vectorAdd(%init, vectorScale(vectorSub(%end, %init), %t)), 0, 1);
	//return  getWord(%init, 0) + (getWord(%end, 0) - getWord(%init, 0) * %t
	//	SPC getWord(%init, 1) + (getWord(%end, 1) - getWord(%init, 1) * %t;
}

function EZ_Lerp1f(%init, %end, %t)
{
	return %init + (%end - %init) * %t;
}