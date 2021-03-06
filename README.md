# TangibleTable

TangibleTable uses reacTIVision technology to track markers around a table and create networks based on the location those markers.
This project implement image wrapping to correct the keystone distorion created when the projector is not placed perperdicularly to a surface.
This is an example of the setup: https://danielcastano.xyz/portfolio#/tangiblebuses/
![](https://s2.gifyu.com/images/TangibleNetwork3.md.gif)
![](https://s2.gifyu.com/images/InteractiveBusNetwork.md.gif)


# Schematic
![](https://static1.squarespace.com/static/5a660c0b1f318d7ab4e9ef29/t/5caf55f9eb39317a0b078d09/1554994689756/schematic.png
)

# App Set up
1. Use the last version of UNITY
2. Place the camera parallel to the projector (both should be projecting and capturing the area you want to work with)
3. Use the reacTIVision fiducial number 17 and 18 to calibrate the image warping.
4. Use the reacTIVision fiducials from number 1 to 15 to create nodes of the network
