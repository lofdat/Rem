![Midori - Promotional - Header 1](http://i.imgur.com/ZVsI0CR.jpg)
Midori is an open-source .NET Core C# bot for Discord.  

# Installation  
* ~~Download and install .NET Core ([x64](https://go.microsoft.com/fwlink/?LinkID=836279), [x86](https://go.microsoft.com/fwlink/?LinkID=836288))~~
* ~~Check that .NET Core is installed from the Command Prompt (`dotnet --version`)~~.
* **If you're using a version from Releases, the correct version of .NET Core is included already for you. :D**
* Download the latest version of Midori from Releases  
* Rename `credentials-template.json` to `credentials.json` and fill in the respective values inside it   
* Open `midori_config.json` and edit to your will  
* Run `MidoriBot.exe` or `_Run MidoriBot` (at the top)  
  
# Building
You need: 
* VS 2015 / MSBuild
* .NET Core

Build instructions coming soon.  
  
# Acknowledgements  
Midori uses and loves:
* .NET Core (.NET Standard) | [GitHub](https://github.com/dotnet/core), [Website](https://dotnet.github.io/)  
* Discord.Net (1.0 release) | [GitHub](https://github.com/RogueException/Discord.Net/), [Documentation](https://discord.foxbot.me/docs/)  
Midori says thanks to:  
* Ruby Rose/Hibiki | [GitHub](https://github.com/Nanabell/Hibiki)  
* Discord API Server | [Discord Invite](https://discord.gg/7ym9k)

# Roadmap
1.3: More commands.  
2.0: Voice support, a database (for storing mutes, guild configurations, the rest).  
2.1: A first-run CLI to enter your credentials and set your configuration. [**This requires Roadmap 2.0 - a database is required for storing first-run data**]  
~~2.2: A first-run GUI (Windows Forms) to enter your credentials and set your configuration.~~ **[WPF is not available in .NET Core]**  
Probably never: A public cloud-hosted version.  
