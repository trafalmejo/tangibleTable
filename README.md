# TangibleTable

TangibleTable uses reacTIVision technology to track markers around a table and create networks based on the location those markers.
This project implement image wrapping to correct the keystone distorion created when the projector is not placed perperdicularly to a surface.
This is an example of the setup: https://danielcastano.xyz/portfolio#/tangiblebuses/
![](https://s2.gifyu.com/images/InteractiveBusNetwork.gif)
![](https://s2.gifyu.com/images/TangibleNetwork3.gif)


# App Set up
1. Use the last version of UNITY
2. Place the camera parallel to the projector (both should be projecting and capturing the area you want to work with)
3. Use the reacTIVision fiducial number 17 and 18 to calibrate the image warping.
4. Use the reacTIVision fiducials from number 1 to 15 to create nodes of the network
