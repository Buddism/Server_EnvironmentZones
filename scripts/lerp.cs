//important vars:
// not: water/sky/ground textures
//but sun flares included

%this.sun.azimuth = %other.var_SunAzimuth;
%this.sun.elevation = %other.var_SunElevation;
%this.sun.color = %other.var_DirectLightColor;
%this.sun.ambient = %other.var_AmbientLightColor;
%this.sun.shadowColor = %other.var_ShadowColor;

%this.sunLight.FlareSize = %other.var_SunFlareSize;
%this.sunLight.color = %other.var_SunFlareColor;
%this.sunLight.setFlareBitmaps ($EnvGuiServer::SunFlare[%other.var_SunFlareTopIdx],$EnvGuiServer::SunFlare[%other.var_SunFlareBottomIdx]);


%this.sky.visibleDistance = %other.var_VisibleDistance;
%this.sky.fogDistance = %other.var_FogDistance;
%this.sky.fogColor = getColorF (%other.var_FogColor);
%this.sky.skyColor = getColorF (%other.var_SkyColor);
%this.sky.windVelocity = %other.var_WindVelocity;
%this.sky.windEffectPrecipitation = %other.var_WindEffectPrecipitation;

%this.waterPlane.setTransform (%pos @ " 0 0 1 0");
%this.waterPlane.scrollSpeed = %other.var_WaterScrollX SPC %other.var_WaterScrollY;
%this.waterPlane.color = getColorI (%other.var_WaterColor);
%this.waterPlane.blend = getWord (%this.waterPlane.color, 3) < 255;
%this.sky.fogVolume1 = %waterVis SPC -10 SPC %height;

%this.waterZone.setTransform (%pos @ " 0 0 1 0");
%this.waterZone.appliedForce = %other.var_WaterScrollX * 414 SPC %other.var_WaterScrollY * -414 SPC 0;
%this.waterZone.setWaterColor (getColorF (%other.var_UnderWaterColor));

%this.groundPlane.color = getColorI (%other.var_GroundColor);
%this.groundPlane.blend = getWord (%this.groundPlane.color, 3) < 255;
%this.groundPlane.scrollSpeed = %other.var_GroundScrollX SPC %other.var_GroundScrollY;

//%this.sky.renderBottomTexture = getWord (%this.groundPlane.color, 3) <= 0;
//%this.sky.noRenderBans = %this.sky.renderBottomTexture;
