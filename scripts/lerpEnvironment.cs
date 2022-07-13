function Environment::initTransition(%this, %other)
{
	if(!isObject(%other))
		return false;

	//short vars:
	// LE - LERP_ENABLED
	// LS - LERP_START
	// LF - LERP_FINAL
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
	if(%thisSun.azimuth			 !$= %otherSun.azimuth		  ) { %this.LES_azimuth			= true;	%this.LSS_azimuth	 	 =	%thisSun.azimuth;				%sunLerps++; } else %this.LES_azimuth	 	 = false;
	if(%thisSun.elevation		 !$= %otherSun.elevation	  ) { %this.LES_elevation		= true;	%this.LSS_elevation	 	 =	%thisSun.elevation;				%sunLerps++; } else %this.LES_elevation	 	 = false;
	if(%thisSun.color			 !$= %otherSun.color		  ) { %this.LES_color			= true;	%this.LSS_color		 	 =	%thisSun.color;					%sunLerps++; } else %this.LES_color		 	 = false;
	if(%thisSun.ambient			 !$= %otherSun.ambient		  ) { %this.LES_ambient			= true;	%this.LSS_ambient	 	 =	%thisSun.ambient;				%sunLerps++; } else %this.LES_ambient	 	 = false;
	if(%thisSun.shadowColor		 !$= %otherSun.shadowColor	  ) { %this.LES_shadowColor		= true;	%this.LSS_shadowColor 	 =	%thisSun.shadowColor;			%sunLerps++; } else %this.LES_shadowColor 	 = false;

	//sunlight
	if(%this.sunLight.FlareSize	 !$= %other.sunLight.FlareSize) { %this.LESL_FlareSize		= true;	%this.LSSL_FlareSize	 =	%this.sunLight.FlareSize;		%sunLight++; } else %this.LESL_FlareSize	 = false;
	if(%this.sunLight.color		 !$= %other.sunLight.color	  ) { %this.LESL_color	 		= true;	%this.LSSL_color	 	 =	%this.sunLight.color; 			%sunLight++; } else %this.LESL_color	 	 = false;

	//sky
	if(%thisSky.visibleDistance	 !$= %otherSky.visibleDistance) { %this.LES_visibleDistance	= true;	%this.LSS_visibleDistance=	%thisSky.visibleDistance;		%skyLerps++; } else %this.LES_visibleDistance= false;
	if(%thisSky.fogDistance		 !$= %otherSky.fogDistance	  ) { %this.LES_fogDistance	 	= true;	%this.LSS_fogDistance	 =	%thisSky.fogDistance;			%skyLerps++; } else %this.LES_fogDistance	 = false;
	if(%thisSky.fogColor		 !$= %otherSky.fogColor		  ) { %this.LES_fogColor		= true;	%this.LSS_fogColor		 =	%thisSky.fogColor;				%skyLerps++; } else %this.LES_fogColor		 = false;
	if(%thisSky.skyColor		 !$= %otherSky.skyColor		  ) { %this.LES_skyColor		= true;	%this.LSS_skyColor		 =	%thisSky.skyColor;				%skyLerps++; } else %this.LES_skyColor		 = false;
	if(%thisSky.fogVolume1		 !$= %otherSky.fogVolume1	  ) { %this.LES_fogVolume1	 	= true;	%this.LSS_fogVolume1	 =	%thisSky.fogVolume1;			%skyLerps++; } else %this.LES_fogVolume1	 = false;
	if(%thisSky.windVelocity 	 !$= %otherSky.windVelocity	  ) { %this.LES_windVelocity	= true;	%this.LSS_windVelocity	 =	%thisSky.windVelocity;			%skyLerps++; } else %this.LES_windVelocity	 = false;

	if( (%thisSky.windEffectPrecipitation || %otherSky.windEffectPrecipitation) )
		%thisSky.windEffectPrecipitation = true;

	if(%thisGP.color 			 !$= %otherGP.color			  ) { %this.LEG_color			= true;	%this.LSG_color			 =  %thisGP.color;					%GPLerps++;	 } else %this.LEG_color			 = false;
	if(%thisGP.scrollSpeed 		 !$= %otherGP.scrollSpeed	  ) { %this.LEG_scrollSpeed	  	= true;	%this.LSG_scrollSpeed	 =  %thisGP.scrollSpeed;			%GPLerps++;	 } else %this.LEG_scrollSpeed	 = false;
	//if(%thisGP.loopsPerUnit 	 !$= %otherGP.loopsPerUnit	  ) { %this.LEG_loopsPerUnit	= true;	%this.LSG_loopsPerUnit	 =  %thisGP.loopsPerUnit;			%GPLerps++;	 } else %this.LEG_loopsPerUnit	 = false;
	if(%thisGP.rayCastColor 	 !$= %otherGP.rayCastColor	  ) { %this.LEG_rayCastColor	= true;	%this.LSG_rayCastColor	 =  %thisGP.rayCastColor;			%GPLerps++;	 } else %this.LEG_rayCastColor	 = false;

	//not sure if cloudHeight is used but im adding- yes it is used
	if(%thisSky.cloudHeight[0]	 !$= %otherSky.cloudHeight[0] ) { %this.LE_cloudHeight[0]	= true;	%this.LS_cloudHeight[0]	 = %thisSky.cloudHeight[0];		  %cloudLerps++; } else %this.LE_cloudHeight[0]	 = false;
	if(%thisSky.cloudHeight[1]	 !$= %otherSky.cloudHeight[1] ) { %this.LE_cloudHeight[1]	= true;	%this.LS_cloudHeight[1]	 = %thisSky.cloudHeight[1];		  %cloudLerps++; } else %this.LE_cloudHeight[1]	 = false;
	if(%thisSky.cloudHeight[2]	 !$= %otherSky.cloudHeight[2] ) { %this.LE_cloudHeight[2]	= true;	%this.LS_cloudHeight[2]	 = %thisSky.cloudHeight[2];		  %cloudLerps++; } else %this.LE_cloudHeight[2]	 = false;
	if(%thisSky.cloudSpeed[0]	 !$= %otherSky.cloudSpeed[0]  ) { %this.LE_cloudSpeed[0]	= true;	%this.LS_cloudSpeed[0]	 = %thisSky.cloudSpeed[0];		  %cloudLerps++; } else %this.LE_cloudSpeed[0]	 = false;
	if(%thisSky.cloudSpeed[1]	 !$= %otherSky.cloudSpeed[1]  ) { %this.LE_cloudSpeed[1]	= true;	%this.LS_cloudSpeed[1]	 = %thisSky.cloudSpeed[1];		  %cloudLerps++; } else %this.LE_cloudSpeed[1]	 = false;
	if(%thisSky.cloudSpeed[2]	 !$= %otherSky.cloudSpeed[2]  ) { %this.LE_cloudSpeed[2]	= true;	%this.LS_cloudSpeed[2]	 = %thisSky.cloudSpeed[2];		  %cloudLerps++; } else %this.LE_cloudSpeed[2]	 = false;

	if(isObject(%thisWP) && isObject(%otherWP))
	{
		if(%thisWP.getPosition() !$= %otherWP.getPosition()	  ) { %this.LEW_position		= true;	%this.LSW_position		 = %thisWP.getTransform(); %this.LFW_position = %otherWP.getPosition(); %WPLerps++;	 } else %this.LEW_position		 = false;
		if(%thisWP.color 		 !$= %otherWP.color			  ) { %this.LEW_color			= true;	%this.LSW_color			 = %thisWP.color;														%WPLerps++;	 } else %this.LEW_color			 = false;
		if(%thisWP.scrollSpeed 	 !$= %otherWP.scrollSpeed	  ) { %this.LEW_scrollSpeed		= true;	%this.LSW_scrollSpeed	 = %thisWP.scrollSpeed;													%WPLerps++;	 } else %this.LEW_scrollSpeed	 = false;
		//if(%thisWP.loopsPerUnit  !$= %otherWP.loopsPerUnit	  ) { %this.LEW_loopsPerUnit	= true;	%this.LSW_loopsPerUnit	 = %thisWP.loopsPerUnit;												%WPLerps++;	 } else %this.LEW_loopsPerUnit	 = false;
		if(%thisWP.rayCastColor  !$= %otherWP.rayCastColor	  ) { %this.LEW_rayCastColor	= true;	%this.LSW_rayCastColor	 = %thisWP.rayCastColor;												%WPLerps++;	 } else %this.LEW_rayCastColor	 = false;
	} else {
		%this.LEW_position		 = false;
		%this.LEW_color			 = false;
		%this.LEW_scrollSpeed	 = false;
		//%this.LEW_loopsPerUnit	 = false;
		%this.LEW_rayCastColor	 = false;

		if(isObject(%thisWP) && !isObject(%otherWP))
		{
			//need to shrink thisWP to nothing
			%this.LEW_position		 = true;
			%this.LSW_position		 = %thisWP.getTransform();
			%this.LFW_position 		 = setWord(%this.LSW_position, 2, -0.5);

			%WPLerps = 1;
		} else if(isObject(%otherWP)) {
			//need to grow thisWP (which doesnt exist yet) to otherWP
			//create our water plane
			%this.copyWaterPlaneFrom(%other);
			%thisWP = %this.waterPlane;

			%startOfTransition = "0 0 -0.5";
			
			%transformRot = getWords(%otherWP.getTransform(), 3, 6);
			%thisWP.setTransform(%startOfTransition SPC %transformRot);

			%this.LSW_position = %startOfTransition SPC %transformRot;
			%this.LFW_position = %otherWP.getTransform();
			%this.LEW_position = true;
			%WPLerps = 1;
		}
	}

	if(isObject(%thisWZ) && isObject(%otherWZ))
	{
		if(%thisWZ.waterColor !$= %otherWZ.waterColor) { %this.LEZ_waterColor = true;	%this.LSZ_waterColor = %thisWZ.waterColor;	%WZLerps++; } else %this.LEZ_waterColor	 = false;

		//physicalzones overlap so im not going to bother
		%thisWZ.setScale(%otherWZ.getScale());
		%thisWZ.setTransform(%otherWZ.getTransform());
		%thisWZ.appliedForce = %otherWZ.appliedForce;
	} else {
		%this.LEZ_waterColor	= false;
		
		if(isObject(%thisWZ)) //%otherWZ doesnt exist if this is true
			%thisWZ.delete();
		else if(isObject(%otherWZ))
			%this.copyWaterZoneFrom(%other);
	}

	if(isObject(%this.rain))
	{
		if(isObject(%other.rain))
		{
			%otherNumDrops = %other.rain.numDrops;
			%thisNumDrops = %this.rain.numDrops;

			//set num drops = %rain.setPercentange( numDrops / %rain.numDrops )
			%this.LER_Percentage = 2; //enabled type 2
			%this.LSR_Percentage = %this.rain.getPercentage();
			//need to update this at 50%
			%this.LFR_Percentage = %otherNumDrops / %thisNumDrops;
		} else {
			//dimish to 0 percentage
			%this.LER_Percentage = 1; //enabled type 1
			%this.LSR_Percentage = %this.rain.getPercentage();
			%this.LFR_Percentage = 0;
		}
	} else {
		if(isObject(%other.rain))
		{
			//%other rain exists, but %this does not
			%this.copyRainFrom(%other);
			%this.LER_Percentage = 1;
			%this.LSR_Percentage = 0;
			%this.LFR_Percentage = %other.rain.getPercentage();
			%this.rain.setPercentange(0);
		} else {
			//neither objects exist
			%this.LER_Percentage = false;
		}
	}

	%this.skyLerps = %skyLerps;
	%this.sunLerps = %sunLerps;
	%this.sunlightLerps = %sunLight;
	%this.WaterZoneLerps = %WZLerps;
	%this.WaterPlaneLerps = %WPLerps;
	%this.cloudLerps = %cloudLerps;
	%this.groundLerps = %GPLerps;

	//talk(%thisSky.fogVolume1 @"//"@ %otherSky.fogVolume1);
	//talk(%this.client.name);
	// talk("skyLerps" SPC %this.skyLerps);
	// talk("sunLerps" SPC %this.sunLerps);
	// talk("sunlightLerps" SPC %this.sunlightLerps);
	// talk("WaterZoneLerps" SPC %this.WaterZoneLerps);
	// talk("WaterPlaneLerps" SPC %this.WaterPlaneLerps);
	// talk("cloudLerps" SPC %this.cloudLerps);
	// talk("groundLerps" SPC %this.groundLerps);

	%totalLerps = %skyLerps + %sunLerps + %sunLight + %WZLerps + %WPLerps + %cloudLerps + %GPLerps;
	if(%totalLerps == 0)
		return false;

	%other.real_vignetteColor = (%other.var_SimpleMode ? %other.simple_VignetteColor : %other.var_VignetteColor);
	%other.real_vignetteMultiply = (%other.var_SimpleMode ? %other.simple_VignetteMultiply : %other.var_VignetteMultiply);
	%this.LE_Vignette = (%other.real_vignetteColor !$= %this.real_vignetteColor);

	return true;
}

function Environment::cancelTransition(%this, %other)
{
	if(!isObject(%other))
		return;

	%lastLerp = %this.lastTransitionValue;
}

function Environment::transitionEnvironment(%this, %other, %lerp)
{
	if(!isObject(%other))
		return;

	%lerp = mClampF(%lerp, 0, 1);
	
	if(%lerp >= 0.5 && %this.lastTransitionValue < 0.5)
	{
		%this.transitionPassedMidpoint(%other, %lerp);
		%sendSkyUpdate = true;

	} else if(%lerp >= 1.0 && %this.lastTransitionValue < 1)
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
	%thisGP = %this.groundPlane;
	%otherGP = %other.groundPlane;

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
		if(%this.LES_azimuth	) { %thisSun.azimuth	 = EZ_LerpAf(%this.LSS_azimuth	   , %otherSun.azimuth	   , %lerp); }
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

	//groundplane
	if(%this.groundLerps > 0)
	{
		if(%this.LEG_color			) { %thisGP.color 		 = EZ_Lerp4i(%this.LSG_color		, %otherGP.color		 , %lerp ); }
		if(%this.LEG_scrollSpeed	) { %thisGP.scrollSpeed  = EZ_Lerp2f(%this.LSG_scrollSpeed	, %otherGP.scrollSpeed	 , %lerp ); }
		//if(%this.LEG_loopsPerUnit	) { %thisGP.loopsPerUnit = EZ_Lerp1f(%this.LSG_loopsPerUnit	, %otherGP.loopsPerUnit	 , %lerp ); }
		if(%this.LEG_rayCastColor	) { %thisGP.rayCastColor = EZ_Lerp1f(%this.LSG_rayCastColor	, %otherGP.rayCastColor	 , %lerp ); }

		%thisGP.blend = getWord (%thisGP.color, 3) < 255;

		%sendGPUpdate = true;
	}

	

	//waterplane
	if(%this.WaterPlaneLerps > 0)
	{
		if(%this.LEW_position		) { %thisWP.setTransform  (EZ_Lerp3f(%this.LSW_position		, %this.LFW_position	 , %lerp ) SPC getWords(%this.LSW_position, 3, 6));	}
		if(%this.LEW_color			) { %thisWP.color 		 = EZ_Lerp4i(%this.LSW_color		, %otherWP.color		 , %lerp ); 										}
		if(%this.LEW_scrollSpeed	) { %thisWP.scrollSpeed  = EZ_Lerp2f(%this.LSW_scrollSpeed	, %otherWP.scrollSpeed	 , %lerp ); 										}
		//if(%this.LEW_loopsPerUnit	) { %thisWP.loopsPerUnit = EZ_Lerp1f(%this.LSW_loopsPerUnit	, %otherWP.loopsPerUnit	 , %lerp ); 										}
		if(%this.LEW_rayCastColor	) { %thisWP.rayCastColor = EZ_Lerp1f(%this.LSW_rayCastColor	, %otherWP.rayCastColor	 , %lerp ); 										}

		%thisWP.blend = getWord (%thisWP.color, 3) < 255;

		%sendWPUpdate = true;
	}

	//waterzone
	if(%this.waterZoneLerps > 0)
	{
		//underwatercolor has like 3 bits per channel & updates at weird amounts
		if(%this.LEZ_waterColor		) { %this.waterZone.setWaterColor(EZ_Lerp4f(%this.LSZ_waterColor	, %other.waterZone.waterColor		, %lerp)); }
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

	if(%this.LER_Percentage		  ) { %this.rain.setPercentange(EZ_Lerp1f(%this.LSR_Percentage	 , %this.LFR_Percentage	   , %lerp)); }

	if(%sendGPUpdate)
		%thisGP.sendUpdate();

	if(%sendWPUpdate)
		%thisWP.sendUpdate();

	if(%sendSkyUpdate)
		%thisSky.sendUpdate();

	%this.lastTransitionValue = %lerp;
}


function Environment::transitionPassedMidpoint(%this, %other, %lerp)
{
	if(!isObject(%other))
		return;
	
	%thisSky = %this.sky;
	%otherSky = %other.sky;
	%thisWP = %this.waterPlane;
	%otherWP = %other.waterPlane;
	%thisGP = %this.groundPlane;
	%otherGP = %other.groundPlane;

	//do texture stuff & non-lerpable
	if(isObject(%otherWP))
	{
		%thisWP.colorMultiply 		 = %otherWP.colorMultiply;
		%thisWP.topTexture			 = %otherWP.topTexture;
		%thisWP.bottomTexture		 = %otherWP.bottomTexture;
	}

	%thisGP.colorMultiply 		 = %otherGP.colorMultiply;
	%thisGP.topTexture			 = %otherGP.topTexture;
	%thisGP.bottomTexture		 = %otherGP.bottomTexture;

	%thisSky.renderBottomTexture = %others.renderBottomTexture;
	%thisSky.noRenderBans		 = %otherSky.noRenderBan;
	%thisSky.materialList		 = %otherSky.materialList;

	%thisGP.loopsPerUnit = %otherGP.loopsPerUnit;
	%thisWP.loopsPerUnit = %otherWP.loopsPerUnit;

	%this.SunLight.setFlareBitmaps (%other.sunLight.localFlareBitmap, %other.sunLight.remoteFlareBitmap);
	
	if(%this.var_SkyIdx !$= %other.var_SkyIdx && isObject(%other.rain))
	{
		%this.rain.delete();
		%this.copyRainFrom(%other);
	}

	%this.var_SkyIdx = %other.var_SkyIdx;
	%this.var_WaterIdx = %other.var_WaterIdx;
	%this.var_GroundIdx = %other.var_GroundIdx;

	%otherNumDrops = %other.rain.numDrops;
	%thisNumDrops = %this.rain.numDrops;

	if(%this.LER_Percentage == 2) //type 2 is update at midpoint
	{
		%curNumDrops = %this.rain.numDrops * %this.rain.getPercentage();
		%this.rain.delete();
		%this.copyRainFrom(%other);

		//set num drops = %rain.setPercentange( numDrops / %rain.numDrops )

		%this.LSR_Percentage = %curNumDrops / %this.rain.numDrops;
		%this.LFR_Percentage = 1;
	}

	//will sendupdate using main function
}

function Environment::transitionEnvironmentFinal(%this, %other)
{
	if(isObject(%this.rain) && !isObject(%other.rain))
		%this.rain.delete();
}

//lerping angles
function EZ_LerpAf(%init, %end, %t)
{
	%dif = %init - %end;
	if(mAbs(%dif) > 180) // the other direction is better
	{
		if(%end > %init)
			%init += 360;
		else
			%end += 360;

		%lerp = (%init + (%end - %init) * %t);

		if(%lerp >= 0 && %lerp <= 360)
			return %lerp;

		//float modulo
		return %lerp - ( (%lerp / 360) | 0) * 360;
	} else {
		return %init + (%end - %init) * %t;
	}
}

//groundplane & waterplane .color dont work with floats at all for some reason
function EZ_Lerp4i(%init, %end, %t)
{
	//init4
	%i = getWord(%init, 3);
	%v = vectorAdd(%init, vectorScale(vectorSub(%end, %init), %t));
	return (getWord(%v, 0) | 0) SPC (getWord(%v, 1) | 0) SPC (getWord(%v, 2) | 0) SPC ((%i + (getWord(%end, 3) - %i) * %t) | 0);
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

function Precipitation::getPercentage(%this)
{
	if(%this.percentage $= "")
		return 1;

	return %this.percentage;
}
package TransitionalEnvironments
{
	function Precipitation::setPercentange(%this, %percent)
	{
		%this.percentage = %percent;
		parent::setPercentange(%this, %percent);
	}
};