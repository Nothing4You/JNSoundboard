# JN Soundboard
A program written in C# using the NAudio library that uses hotkeys to play sounds into a chosen sound device. It is similar to [EXP Soundboard](https://sourceforge.net/projects/expsoundboard/), except that JN Soundboard is not as cross-platform as EXP, but, there are more features in JN than EXP.

Features:
* Can play MP3, WAV, WMA, and AIFF files
* Play sounds through any sound device (speakers, virtual audio cable, etc.)
* Microphone loopback (loops microphone sound through playback device)
* Add, edit, remove, and clear key combinations
* Can play a random sound out of multiple (just select multiple files when adding a hotkey)
* Save (and load) hotkeys to XML file
* hotkey to stop currently playing sound
* hotkeys to load XML file containing hotkeys
* Text-to-speech

Requires: 
* .NET Framework 4.5.2
* NAudio

How to play sound effects over microphone:
You can really play it "over" the microphone, however you can route them both through a virtual audio cable.
To do that, first install a virtual audio cable (I recommend [VB-CABLE](http://vb-audio.pagesperso-orange.fr/Cable/index.htm)), set the playback device to the virtual audio cable, then set the loopback device to your microphone.
Lastly, in the application that is going to use the microphone, set the microphone device to "VB-Audio Virtual Cable".

Screenshots: 

![Main window](https://i.imgur.com/aqntN58.png)
![Add keys window](https://i.imgur.com/JklV0mA.png)
![Settings window](https://i.imgur.com/sSxR7Uu.png)
![Text-to-speech window](https://i.imgur.com/EoPayHn.png)
