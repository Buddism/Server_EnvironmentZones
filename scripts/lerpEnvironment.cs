function Environment::initLerp(%this, %other)
{
	//short vars for performance
	// LE - LERP_ENABLED
	// LS - LERP_START
	//overlapping var names will be differentiatied by short name of the object

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
	if(%thisSun.azimuth					!$=	%otherSun.azimuth				) {	%this.LE_azimuth	 	 = true;	%this.LS_azimuth	 	 =	%thisSun.azimuth;				} else %this.LE_azimuth	 	 	 = false;
	if(%thisSun.elevation				!$=	%otherSun.elevation				) {	%this.LE_elevation	 	 = true;	%this.LS_elevation	 	 =	%thisSun.elevation;				} else %this.LE_elevation	 	 = false;
	if(%thisSun.color					!$=	%otherSun.color					) {	%this.LES_color		 	 = true;	%this.LSS_color		 	 =	%thisSun.color;					} else %this.LES_color		 	 = false;
	if(%thisSun.ambient					!$=	%otherSun.ambient				) {	%this.LE_ambient	 	 = true;	%this.LS_ambient	 	 =	%thisSun.ambient;				} else %this.LE_ambient	 	 	 = false;
	if(%thisSun.shadowColor				!$=	%otherSun.shadowColor			) {	%this.LE_shadowColor 	 = true;	%this.LS_shadowColor 	 =	%thisSun.shadowColor;			} else %this.LE_shadowColor 	 = false;

	//sunlight
	if(%this.sunLight.FlareSize			!$= %otherSun.sunLight.FlareSize	) { %this.LE_FlareSize	 	 = true;	%this.LS_FlareSize	 	 =	%this.sunLight.FlareSize;		} else %this.LE_FlareSize	 	 = false;
	if(%this.sunLight.color				!$= %otherSun.sunLight.color		) { %this.LESL_color	 	 = true;	%this.LSSL_color	 	 =	%this.sunLight.color; 			} else %this.LESL_color	 		 = false;

	//sky
	if(%thisSky.visibleDistance			!$=	%otherSky.visibleDistance		) {	%this.LE_visibleDistance = true;	%this.LS_visibleDistance =	%thisSky.visibleDistance;		} else %this.LE_visibleDistance  = false;
	if(%thisSky.fogDistance				!$=	%otherSky.fogDistance			) {	%this.LE_fogDistance	 = true;	%this.LS_fogDistance	 =	%thisSky.fogDistance;			} else %this.LE_fogDistance	 	 = false;
	if(%thisSky.fogColor				!$=	%otherSky.fogColor				) {	%this.LE_fogColor		 = true;	%this.LS_fogColor		 =	%thisSky.fogColor;				} else %this.LE_fogColor		 = false;
	if(%thisSky.skyColor				!$=	%otherSky.skyColor				) {	%this.LE_skyColor		 = true;	%this.LS_skyColor		 =	%thisSky.skyColor;				} else %this.LE_skyColor		 = false;

	//special handling required
	if(%thisSky.windVelocity 			!$= %otherSky.windVelocity			) {	%this.LE_windVelocity	 = true;	%this.LS_windVelocity	 =	%thisSky.windVelocity;			} else %this.LE_windVelocity	 = false;
	if(%thisSky.windEffectPrecipitation	!= %otherSky.windEffectPrecipitation) { %this.LE_WEP 			 = true;	%this.LE_WEP			 = %thisSky.windEffectPrecipitation;} else %this.LE_WEP 			 = false;

	if(%thisGP.color 					!$= %otherGP.color					) {	%this.LEG_color			 = true;	%this.LS_color			= %thisGP.color;					} else %this.LEG_color			 = false;
	if(%thisGP.blend 					!$= %otherGP.blend					) {	%this.LEG_blend			 = true;	%this.LS_blend			= %thisGP.blend;					} else %this.LEG_blend			 = false;
	if(%thisGP.scrollSpeed 				!$= %otherGP.scrollSpeed			) {	%this.LEG_scrollSpeed	 = true;	%this.LS_scrollSpeed	= %thisGP.scrollSpeed;				} else %this.LEG_scrollSpeed	 = false;
	if(%thisGP.loopsPerUnit 			!$= %otherGP.loopsPerUnit			) {	%this.LEG_loopsPerUnit	 = true;	%this.LS_loopsPerUnit	= %thisGP.loopsPerUnit;				} else %this.LEG_loopsPerUnit	 = false;
	if(%thisGP.colorMultiply 			!$= %otherGP.colorMultiply			) {	%this.LEG_colorMultiply	 = true;	%this.LS_colorMultiply	= %thisGP.colorMultiply;			} else %this.LEG_colorMultiply	 = false;
	if(%thisGP.rayCastColor 			!$= %otherGP.rayCastColor			) {	%this.LEG_rayCastColor	 = true;	%this.LS_rayCastColor	= %thisGP.rayCastColor;				} else %this.LEG_rayCastColor	 = false;

	//TODO: OBJECT EXISTANCE STUFF
	if(%thisWP.getTransform() 			!$= %otherWP.getTransform()			) { %this.LEW_getTransform	 = true;	%this.LS_getTransform	= %thisWP.getTransform();			} else %this.LEW_getTransform	 = false;
	if(%thisWP.color 					!$= %otherWP.color					) { %this.LEW_color			 = true;	%this.LS_color			= %thisWP.color;					} else %this.LEW_color			 = false;
	if(%thisWP.blend 					!$= %otherWP.blend					) { %this.LEW_blend			 = true;	%this.LS_blend			= %thisWP.blend;					} else %this.LEW_blend			 = false;
	if(%thisWP.scrollSpeed 				!$= %otherWP.scrollSpeed			) { %this.LEW_scrollSpeed	 = true;	%this.LS_scrollSpeed	= %thisWP.scrollSpeed;				} else %this.LEW_scrollSpeed	 = false;
	if(%thisWP.loopsPerUnit 			!$= %otherWP.loopsPerUnit			) { %this.LEW_loopsPerUnit	 = true;	%this.LS_loopsPerUnit	= %thisWP.loopsPerUnit;				} else %this.LEW_loopsPerUnit	 = false;
	if(%thisWP.colorMultiply 			!$= %otherWP.colorMultiply			) { %this.LEW_colorMultiply	 = true;	%this.LS_colorMultiply	= %thisWP.colorMultiply;			} else %this.LEW_colorMultiply	 = false;
	if(%thisWP.rayCastColor 			!$= %otherWP.rayCastColor			) { %this.LEW_rayCastColor	 = true;	%this.LS_rayCastColor	= %thisWP.rayCastColor;				} else %this.LEW_rayCastColor	 = false;

	if(%this.waterZone.getTransform() 	!$= %other.waterZone.getTransform()	) { %this.LE_getTransform	 = true;	%this.LS_getTransform	= %this.waterZone.getTransform();	} else %this.LE_getTransform	 = false;
	if(%this.waterZone.appliedForce 	!$= %other.waterZone.appliedForce	) { %this.LE_appliedForce	 = true;	%this.LS_appliedForce	= %this.waterZone.appliedForce;		} else %this.LE_appliedForce	 = false;
	if(%this.waterZone.setWaterColor 	!$= %other.waterZone.setWaterColor	) { %this.LE_setWaterColor	 = true;	%this.LS_setWaterColor	= %this.waterZone.setWaterColor;	} else %this.LE_setWaterColor	 = false;

	//not sure if cloudHeight is used but im adding it for now
	if(%thisSky.cloudHeight[0]			!$=	%otherSky.cloudHeight[0]		) {	%this.LE_cloudHeight[0]	 = true;	%this.LS_cloudHeight[0]	= %otherSky.cloudHeight[0];			} else %this.LE_cloudHeight[0]	 = false;
	if(%thisSky.cloudHeight[1]			!$=	%otherSky.cloudHeight[1]		) {	%this.LE_cloudHeight[1]	 = true;	%this.LS_cloudHeight[1]	= %otherSky.cloudHeight[1];			} else %this.LE_cloudHeight[1]	 = false;
	if(%thisSky.cloudHeight[2]			!$=	%otherSky.cloudHeight[2]		) {	%this.LE_cloudHeight[2]	 = true;	%this.LS_cloudHeight[2]	= %otherSky.cloudHeight[2];			} else %this.LE_cloudHeight[2]	 = false;
	if(%thisSky.cloudSpeed[0]			!$=	%otherSky.cloudSpeed[0]			) {	%this.LE_cloudSpeed[0]	 = true;	%this.LS_cloudSpeed[0]	= %otherSky.cloudSpeed[0];			} else %this.LE_cloudSpeed[0]	 = false;
	if(%thisSky.cloudSpeed[1]			!$=	%otherSky.cloudSpeed[1]			) {	%this.LE_cloudSpeed[1]	 = true;	%this.LS_cloudSpeed[1]	= %otherSky.cloudSpeed[1];			} else %this.LE_cloudSpeed[1]	 = false;
	if(%thisSky.cloudSpeed[2]			!$=	%otherSky.cloudSpeed[2]			) {	%this.LE_cloudSpeed[2]	 = true;	%this.LS_cloudSpeed[2]	= %otherSky.cloudSpeed[2];			} else %this.LE_cloudSpeed[2]	 = false;
}

function EZ_Lerp4f(%init, %end, %t)
{
	if(%t > 1)
		return %end;

	//init4
	%i4 = getWord(%init, 0);

	//lerp4
	%l4 = %i4 + (getWord(%end, 4) - %i4) * %t;

	return vectorAdd(%init, vectorScale(vectorSub(%end, %init), %t)) SPC %l4;
}

function EZ_Lerp3f(%init, %end, %t)
{
	if(%t > 1)
		return %end;

	return vectorAdd(%init, vectorScale(vectorSub(%end, %init), %t));
}

function EZ_Lerp2f(%init, %end, %t)
{
	if(%t > 1)
		return %end;

	//dont feel like benchmarking this
	return getWords(vectorAdd(%init, vectorScale(vectorSub(%end, %init), %t)), 0, 1);
	//return  getWord(%init, 0) + (getWord(%end, 0) - getWord(%init, 0) * %t
	//	SPC getWord(%init, 1) + (getWord(%end, 1) - getWord(%init, 1) * %t;
}

function EZ_Lerp(%init, %end, %t)
{
	if(%t > 1)
		return %end;

	return %init + (%end - %init) * %t;
}