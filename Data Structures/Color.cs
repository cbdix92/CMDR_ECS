using System;


namespace CMDR
{
    public struct Color : IEquatable<Color>
    {
        public float R { get; set; }
        public float G { get; set; }
        public float B { get; set; }
        public float A { get; set; }


        #region CONSTANTS

		public static readonly Color Transparent			   = new Color(0);
        public static readonly Color Black                     = HexToColor(0x000000);
		public static readonly Color Night                     = HexToColor(0x0C090A);
		public static readonly Color Charcoal                  = HexToColor(0x34282C);
		public static readonly Color Oil                       = HexToColor(0x3B3131);
		public static readonly Color DarkGray                  = HexToColor(0x3A3B3C);
		public static readonly Color LightBlack                = HexToColor(0x454545);
		public static readonly Color BlackCat                  = HexToColor(0x413839);
		public static readonly Color Iridium                   = HexToColor(0x3D3C3A);
		public static readonly Color BlackEel                  = HexToColor(0x463E3F);
		public static readonly Color BlackCow                  = HexToColor(0x4C4646);
		public static readonly Color GrayWolf                  = HexToColor(0x504A4B);
		public static readonly Color VampireGray               = HexToColor(0x565051);
		public static readonly Color IronGray                  = HexToColor(0x52595D);
		public static readonly Color GrayDolphin               = HexToColor(0x5C5858);
		public static readonly Color CarbonGray                = HexToColor(0x625D5D);
		public static readonly Color AshGray                   = HexToColor(0x666362);
		public static readonly Color CloudyGray                = HexToColor(0x6D6968);
		public static readonly Color DimGray                   = HexToColor(0x696969);
		public static readonly Color SmokeyGray                = HexToColor(0x726E6D);
		public static readonly Color AlienGray                 = HexToColor(0x736F6E);
		public static readonly Color SonicSilver               = HexToColor(0x757575);
		public static readonly Color PlatinumGray              = HexToColor(0x797979);
		public static readonly Color Granite                   = HexToColor(0x837E7C);
		public static readonly Color Gray                      = HexToColor(0x808080);
		public static readonly Color BattleshipGray            = HexToColor(0x848482);
		public static readonly Color GrayCloud                 = HexToColor(0xB6B6B4);
		public static readonly Color Silver                    = HexToColor(0xC0C0C0);
		public static readonly Color PaleSilver                = HexToColor(0xC9C0BB);
		public static readonly Color GrayGoose                 = HexToColor(0xD1D0CE);
		public static readonly Color PlatinumSilver            = HexToColor(0xCECECE);
		public static readonly Color LightGray                 = HexToColor(0xD3D3D3);
		public static readonly Color Gainsboro                 = HexToColor(0xDCDCDC);
		public static readonly Color Platinum                  = HexToColor(0xE5E4E2);
		public static readonly Color MetallicSilver            = HexToColor(0xBCC6CC);
		public static readonly Color BlueGray                  = HexToColor(0x98AFC7);
		public static readonly Color RomanSilver               = HexToColor(0x838996);
		public static readonly Color LightSlateGray            = HexToColor(0x778899);
		public static readonly Color SlateGray                 = HexToColor(0x708090);
		public static readonly Color RatGray                   = HexToColor(0x6D7B8D);
		public static readonly Color SlateGraniteGray          = HexToColor(0x657383);
		public static readonly Color JetGray                   = HexToColor(0x616D7E);
		public static readonly Color MistBlue                  = HexToColor(0x646D7E);
		public static readonly Color MarbleBlue                = HexToColor(0x566D7E);
		public static readonly Color SlateBlueGrey             = HexToColor(0x737CA1);
		public static readonly Color LightPurpleBlue           = HexToColor(0x728FCE);
		public static readonly Color AzureBlue                 = HexToColor(0x4863A0);
		public static readonly Color BlueJay                   = HexToColor(0x2B547E);
		public static readonly Color CharcoalBlue              = HexToColor(0x36454F);
		public static readonly Color DarkBlueGrey              = HexToColor(0x29465B);
		public static readonly Color DarkSlate                 = HexToColor(0x2B3856);
		public static readonly Color DeepSeaBlue               = HexToColor(0x123456);
		public static readonly Color NightBlue                 = HexToColor(0x151B54);
		public static readonly Color MidnightBlue              = HexToColor(0x191970);
		public static readonly Color Navy                      = HexToColor(0x000080);
		public static readonly Color DenimDarkBlue             = HexToColor(0x151B8D);
		public static readonly Color DarkBlue                  = HexToColor(0x00008B);
		public static readonly Color LapisBlue                 = HexToColor(0x15317E);
		public static readonly Color NewMidnightBlue           = HexToColor(0x0000A0);
		public static readonly Color EarthBlue                 = HexToColor(0x0000A5);
		public static readonly Color CobaltBlue                = HexToColor(0x0020C2);
		public static readonly Color MediumBlue                = HexToColor(0x0000CD);
		public static readonly Color BlueberryBlue             = HexToColor(0x0041C2);
		public static readonly Color CanaryBlue                = HexToColor(0x2916F5);
		public static readonly Color Blue                      = HexToColor(0x0000FF);
		public static readonly Color BrightBlue                = HexToColor(0x0909FF);
		public static readonly Color BlueOrchid                = HexToColor(0x1F45FC);
		public static readonly Color SapphireBlue              = HexToColor(0x2554C7);
		public static readonly Color BlueEyes                  = HexToColor(0x1569C7);
		public static readonly Color BrightNavyBlue            = HexToColor(0x1974D2);
		public static readonly Color BalloonBlue               = HexToColor(0x2B60DE);
		public static readonly Color RoyalBlue                 = HexToColor(0x4169E1);
		public static readonly Color OceanBlue                 = HexToColor(0x2B65EC);
		public static readonly Color BlueRibbon                = HexToColor(0x306EFF);
		public static readonly Color BlueDress                 = HexToColor(0x157DEC);
		public static readonly Color NeonBlue                  = HexToColor(0x1589FF);
		public static readonly Color DodgerBlue                = HexToColor(0x1E90FF);
		public static readonly Color GlacialBlueIce            = HexToColor(0x368BC1);
		public static readonly Color SteelBlue                 = HexToColor(0x4682B4);
		public static readonly Color SilkBlue                  = HexToColor(0x488AC7);
		public static readonly Color WindowsBlue               = HexToColor(0x357EC7);
		public static readonly Color BlueIvy                   = HexToColor(0x3090C7);
		public static readonly Color BlueKoi                   = HexToColor(0x659EC7);
		public static readonly Color ColumbiaBlue              = HexToColor(0x87AFC7);
		public static readonly Color BabyBlue                  = HexToColor(0x95B9C7);
		public static readonly Color CornflowerBlue            = HexToColor(0x6495ED);
		public static readonly Color SkyBlueDress              = HexToColor(0x6698FF);
		public static readonly Color Iceberg                   = HexToColor(0x56A5EC);
		public static readonly Color ButterflyBlue             = HexToColor(0x38ACEC);
		public static readonly Color DeepSkyBlue               = HexToColor(0x00BFFF);
		public static readonly Color MiddayBlue                = HexToColor(0x3BB9FF);
		public static readonly Color CrystalBlue               = HexToColor(0x5CB3FF);
		public static readonly Color DenimBlue                 = HexToColor(0x79BAEC);
		public static readonly Color DaySkyBlue                = HexToColor(0x82CAFF);
		public static readonly Color LightSkyBlue              = HexToColor(0x87CEFA);
		public static readonly Color SkyBlue                   = HexToColor(0x87CEEB);
		public static readonly Color JeansBlue                 = HexToColor(0xA0CFEC);
		public static readonly Color BlueAngel                 = HexToColor(0xB7CEEC);
		public static readonly Color PastelBlue                = HexToColor(0xB4CFEC);
		public static readonly Color LightDayBlue              = HexToColor(0xADDFFF);
		public static readonly Color SeaBlue                   = HexToColor(0xC2DFFF);
		public static readonly Color HeavenlyBlue              = HexToColor(0xC6DEFF);
		public static readonly Color RobinEggBlue              = HexToColor(0xBDEDFF);
		public static readonly Color PowderBlue                = HexToColor(0xB0E0E6);
		public static readonly Color CoralBlue                 = HexToColor(0xAFDCEC);
		public static readonly Color LightBlue                 = HexToColor(0xADD8E6);
		public static readonly Color LightSteelBlue            = HexToColor(0xB0CFDE);
		public static readonly Color GulfBlue                  = HexToColor(0xC9DFEC);
		public static readonly Color PastelLightBlue           = HexToColor(0xD5D6EA);
		public static readonly Color LavenderBlue              = HexToColor(0xE3E4FA);
		public static readonly Color Lavender                  = HexToColor(0xE6E6FA);
		public static readonly Color Water                     = HexToColor(0xEBF4FA);
		public static readonly Color AliceBlue                 = HexToColor(0xF0F8FF);
		public static readonly Color GhostWhite                = HexToColor(0xF8F8FF);
		public static readonly Color Azure                     = HexToColor(0xF0FFFF);
		public static readonly Color LightCyan                 = HexToColor(0xE0FFFF);
		public static readonly Color LightSlate                = HexToColor(0xCCFFFF);
		public static readonly Color ElectricBlue              = HexToColor(0x9AFEFF);
		public static readonly Color TronBlue                  = HexToColor(0x7DFDFE);
		public static readonly Color BlueZircon                = HexToColor(0x57FEFF);
		public static readonly Color Cyan                      = HexToColor(0x00FFFF);
		public static readonly Color BrightCyan                = HexToColor(0x0AFFFF);
		public static readonly Color Celeste                   = HexToColor(0x50EBEC);
		public static readonly Color BlueDiamond               = HexToColor(0x4EE2EC);
		public static readonly Color BrightTurquoise           = HexToColor(0x16E2F5);
		public static readonly Color BlueLagoon                = HexToColor(0x8EEBEC);
		public static readonly Color PaleTurquoise             = HexToColor(0xAFEEEE);
		public static readonly Color PaleBlueLily              = HexToColor(0xCFECEC);
		public static readonly Color TiffanyBlue               = HexToColor(0x81D8D0);
		public static readonly Color BlueHosta                 = HexToColor(0x77BFC7);
		public static readonly Color CyanOpaque                = HexToColor(0x92C7C7);
		public static readonly Color NorthernLightsBlue        = HexToColor(0x78C7C7);
		public static readonly Color BlueGreen                 = HexToColor(0x7BCCB5);
		public static readonly Color MediumAquaMarine          = HexToColor(0x66CDAA);
		public static readonly Color MagicMint                 = HexToColor(0xAAF0D1);
		public static readonly Color Aquamarine                = HexToColor(0x7FFFD4);
		public static readonly Color LightAquamarine           = HexToColor(0x93FFE8);
		public static readonly Color Turquoise                 = HexToColor(0x40E0D0);
		public static readonly Color MediumTurquoise           = HexToColor(0x48D1CC);
		public static readonly Color DeepTurquoise             = HexToColor(0x48CCCD);
		public static readonly Color Jellyfish                 = HexToColor(0x46C7C7);
		public static readonly Color BlueTurquoise             = HexToColor(0x43C6DB);
		public static readonly Color DarkTurquoise             = HexToColor(0x00CED1);
		public static readonly Color MacawBlueGreen            = HexToColor(0x43BFC7);
		public static readonly Color LightSeaGreen             = HexToColor(0x20B2AA);
		public static readonly Color SeafoamGreen              = HexToColor(0x3EA99F);
		public static readonly Color CadetBlue                 = HexToColor(0x5F9EA0);
		public static readonly Color DeepSea                   = HexToColor(0x3B9C9C);
		public static readonly Color DarkCyan                  = HexToColor(0x008B8B);
		public static readonly Color Teal                      = HexToColor(0x008080);
		public static readonly Color MediumTeal                = HexToColor(0x045F5F);
		public static readonly Color DeepTeal                  = HexToColor(0x033E3E);
		public static readonly Color DarkSlateGray             = HexToColor(0x25383C);
		public static readonly Color Gunmetal                  = HexToColor(0x2C3539);
		public static readonly Color BlueMossGreen             = HexToColor(0x3C565B);
		public static readonly Color BeetleGreen               = HexToColor(0x4C787E);
		public static readonly Color GrayishTurquoise          = HexToColor(0x5E7D7E);
		public static readonly Color GreenishBlue              = HexToColor(0x307D7E);
		public static readonly Color AquamarineStone           = HexToColor(0x348781);
		public static readonly Color SeaTurtleGreen            = HexToColor(0x438D80);
		public static readonly Color DullSeaGreen              = HexToColor(0x4E8975);
		public static readonly Color DeepSeaGreen              = HexToColor(0x306754);
		public static readonly Color SeaGreen                  = HexToColor(0x2E8B57);
		public static readonly Color DarkMint                  = HexToColor(0x31906E);
		public static readonly Color Jade                      = HexToColor(0x00A36C);
		public static readonly Color EarthGreen                = HexToColor(0x34A56F);
		public static readonly Color Emerald                   = HexToColor(0x50C878);
		public static readonly Color Mint                      = HexToColor(0x3EB489);
		public static readonly Color MediumSeaGreen            = HexToColor(0x3CB371);
		public static readonly Color CamouflageGreen           = HexToColor(0x78866B);
		public static readonly Color SageGreen                 = HexToColor(0x848B79);
		public static readonly Color HazelGreen                = HexToColor(0x617C58);
		public static readonly Color VenomGreen                = HexToColor(0x728C00);
		public static readonly Color OliveDrab                 = HexToColor(0x6B8E23);
		public static readonly Color Olive                     = HexToColor(0x808000);
		public static readonly Color DarkOliveGreen            = HexToColor(0x556B2F);
		public static readonly Color ArmyGreen                 = HexToColor(0x4B5320);
		public static readonly Color FernGreen                 = HexToColor(0x667C26);
		public static readonly Color FallForestGreen           = HexToColor(0x4E9258);
		public static readonly Color PineGreen                 = HexToColor(0x387C44);
		public static readonly Color MediumForestGreen         = HexToColor(0x347235);
		public static readonly Color JungleGreen               = HexToColor(0x347C2C);
		public static readonly Color ForestGreen               = HexToColor(0x228B22);
		public static readonly Color Green                     = HexToColor(0x008000);
		public static readonly Color DarkGreen                 = HexToColor(0x006400);
		public static readonly Color DeepEmeraldGreen          = HexToColor(0x046307);
		public static readonly Color DarkForestGreen           = HexToColor(0x254117);
		public static readonly Color SeaweedGreen              = HexToColor(0x437C17);
		public static readonly Color ShamrockGreen             = HexToColor(0x347C17);
		public static readonly Color GreenOnion                = HexToColor(0x6AA121);
		public static readonly Color GreenPepper               = HexToColor(0x4AA02C);
		public static readonly Color DarkLimeGreen             = HexToColor(0x41A317);
		public static readonly Color ParrotGreen               = HexToColor(0x12AD2B);
		public static readonly Color CloverGreen               = HexToColor(0x3EA055);
		public static readonly Color DinosaurGreen             = HexToColor(0x73A16C);
		public static readonly Color GreenSnake                = HexToColor(0x6CBB3C);
		public static readonly Color AlienGreen                = HexToColor(0x6CC417);
		public static readonly Color GreenApple                = HexToColor(0x4CC417);
		public static readonly Color LimeGreen                 = HexToColor(0x32CD32);
		public static readonly Color PeaGreen                  = HexToColor(0x52D017);
		public static readonly Color KellyGreen                = HexToColor(0x4CC552);
		public static readonly Color ZombieGreen               = HexToColor(0x54C571);
		public static readonly Color FrogGreen                 = HexToColor(0x99C68E);
		public static readonly Color DarkSeaGreen              = HexToColor(0x8FBC8F);
		public static readonly Color GreenPeas                 = HexToColor(0x89C35C);
		public static readonly Color DollarBillGreen           = HexToColor(0x85BB65);
		public static readonly Color IguanaGreen               = HexToColor(0x9CB071);
		public static readonly Color AcidGreen                 = HexToColor(0xB0BF1A);
		public static readonly Color AvocadoGreen              = HexToColor(0xB2C248);
		public static readonly Color PistachioGreen            = HexToColor(0x9DC209);
		public static readonly Color SaladGreen                = HexToColor(0xA1C935);
		public static readonly Color YellowGreen               = HexToColor(0x9ACD32);
		public static readonly Color PastelGreen               = HexToColor(0x77DD77);
		public static readonly Color HummingbirdGreen          = HexToColor(0x7FE817);
		public static readonly Color NebulaGreen               = HexToColor(0x59E817);
		public static readonly Color StoplightGoGreen          = HexToColor(0x57E964);
		public static readonly Color NeonGreen                 = HexToColor(0x16F529);
		public static readonly Color JadeGreen                 = HexToColor(0x5EFB6E);
		public static readonly Color LimeMintGreen             = HexToColor(0x36F57F);
		public static readonly Color SpringGreen               = HexToColor(0x00FF7F);
		public static readonly Color MediumSpringGreen         = HexToColor(0x00FA9A);
		public static readonly Color EmeraldGreen              = HexToColor(0x5FFB17);
		public static readonly Color Lime                      = HexToColor(0x00FF00);
		public static readonly Color LawnGreen                 = HexToColor(0x7CFC00);
		public static readonly Color BrightGreen               = HexToColor(0x66FF00);
		public static readonly Color Chartreuse                = HexToColor(0x7FFF00);
		public static readonly Color YellowLawnGreen           = HexToColor(0x87F717);
		public static readonly Color AloeVeraGreen             = HexToColor(0x98F516);
		public static readonly Color DullGreenYellow           = HexToColor(0xB1FB17);
		public static readonly Color GreenYellow               = HexToColor(0xADFF2F);
		public static readonly Color ChameleonGreen            = HexToColor(0xBDF516);
		public static readonly Color NeonYellowGreen           = HexToColor(0xDAEE01);
		public static readonly Color YellowGreenGrosbeak       = HexToColor(0xE2F516);
		public static readonly Color TeaGreen                  = HexToColor(0xCCFB5D);
		public static readonly Color SlimeGreen                = HexToColor(0xBCE954);
		public static readonly Color AlgaeGreen                = HexToColor(0x64E986);
		public static readonly Color LightGreen                = HexToColor(0x90EE90);
		public static readonly Color DragonGreen               = HexToColor(0x6AFB92);
		public static readonly Color PaleGreen                 = HexToColor(0x98FB98);
		public static readonly Color MintGreen                 = HexToColor(0x98FF98);
		public static readonly Color GreenThumb                = HexToColor(0xB5EAAA);
		public static readonly Color OrganicBrown              = HexToColor(0xE3F9A6);
		public static readonly Color LightJade                 = HexToColor(0xC3FDB8);
		public static readonly Color LightRoseGreen            = HexToColor(0xDBF9DB);
		public static readonly Color HoneyDew                  = HexToColor(0xF0FFF0);
		public static readonly Color MintCream                 = HexToColor(0xF5FFFA);
		public static readonly Color LemonChiffon              = HexToColor(0xFFFACD);
		public static readonly Color Parchment                 = HexToColor(0xFFFFC2);
		public static readonly Color Cream                     = HexToColor(0xFFFFCC);
		public static readonly Color LightGoldenRodYellow      = HexToColor(0xFAFAD2);
		public static readonly Color LightYellow               = HexToColor(0xFFFFE0);
		public static readonly Color Beige                     = HexToColor(0xF5F5DC);
		public static readonly Color Cornsilk                  = HexToColor(0xFFF8DC);
		public static readonly Color Blonde                    = HexToColor(0xFBF6D9);
		public static readonly Color Champagne                 = HexToColor(0xF7E7CE);
		public static readonly Color AntiqueWhite              = HexToColor(0xFAEBD7);
		public static readonly Color PapayaWhip                = HexToColor(0xFFEFD5);
		public static readonly Color BlanchedAlmond            = HexToColor(0xFFEBCD);
		public static readonly Color Bisque                    = HexToColor(0xFFE4C4);
		public static readonly Color Wheat                     = HexToColor(0xF5DEB3);
		public static readonly Color Moccasin                  = HexToColor(0xFFE4B5);
		public static readonly Color Peach                     = HexToColor(0xFFE5B4);
		public static readonly Color LightOrange               = HexToColor(0xFED8B1);
		public static readonly Color PeachPuff                 = HexToColor(0xFFDAB9);
		public static readonly Color NavajoWhite               = HexToColor(0xFFDEAD);
		public static readonly Color GoldenBlonde              = HexToColor(0xFBE7A1);
		public static readonly Color GoldenSilk                = HexToColor(0xF3E3C3);
		public static readonly Color DarkBlonde                = HexToColor(0xF0E2B6);
		public static readonly Color LightGold                 = HexToColor(0xF1E5AC);
		public static readonly Color Vanilla                   = HexToColor(0xF3E5AB);
		public static readonly Color TanBrown                  = HexToColor(0xECE5B6);
		public static readonly Color PaleGoldenRod             = HexToColor(0xEEE8AA);
		public static readonly Color Khaki                     = HexToColor(0xF0E68C);
		public static readonly Color CardboardBrown            = HexToColor(0xEDDA74);
		public static readonly Color HarvestGold               = HexToColor(0xEDE275);
		public static readonly Color SunYellow                 = HexToColor(0xFFE87C);
		public static readonly Color CornYellow                = HexToColor(0xFFF380);
		public static readonly Color PastelYellow              = HexToColor(0xFAF884);
		public static readonly Color NeonYellow                = HexToColor(0xFFFF33);
		public static readonly Color Yellow                    = HexToColor(0xFFFF00);
		public static readonly Color CanaryYellow              = HexToColor(0xFFEF00);
		public static readonly Color BananaYellow              = HexToColor(0xF5E216);
		public static readonly Color MustardYellow             = HexToColor(0xFFDB58);
		public static readonly Color GoldenYellow              = HexToColor(0xFFDF00);
		public static readonly Color BoldYellow                = HexToColor(0xF9DB24);
		public static readonly Color RubberDuckyYellow         = HexToColor(0xFFD801);
		public static readonly Color Gold                      = HexToColor(0xFFD700);
		public static readonly Color BrightGold                = HexToColor(0xFDD017);
		public static readonly Color GoldenBrown               = HexToColor(0xEAC117);
		public static readonly Color DeepYellow                = HexToColor(0xF6BE00);
		public static readonly Color MacaroniandCheese         = HexToColor(0xF2BB66);
		public static readonly Color Saffron                   = HexToColor(0xFBB917);
		public static readonly Color Beer                      = HexToColor(0xFBB117);
		public static readonly Color YellowOrange              = HexToColor(0xFFAE42);
		public static readonly Color Cantaloupe                = HexToColor(0xFFA62F);
		public static readonly Color Orange                    = HexToColor(0xFFA500);
		public static readonly Color BrownSand                 = HexToColor(0xEE9A4D);
		public static readonly Color SandyBrown                = HexToColor(0xF4A460);
		public static readonly Color BrownSugar                = HexToColor(0xE2A76F);
		public static readonly Color CamelBrown                = HexToColor(0xC19A6B);
		public static readonly Color DeerBrown                 = HexToColor(0xE6BF83);
		public static readonly Color BurlyWood                 = HexToColor(0xDEB887);
		public static readonly Color Tan                       = HexToColor(0xD2B48C);
		public static readonly Color LightFrenchBeige          = HexToColor(0xC8AD7F);
		public static readonly Color Sand                      = HexToColor(0xC2B280);
		public static readonly Color Sage                      = HexToColor(0xBCB88A);
		public static readonly Color FallLeafBrown             = HexToColor(0xC8B560);
		public static readonly Color GingerBrown               = HexToColor(0xC9BE62);
		public static readonly Color DarkKhaki                 = HexToColor(0xBDB76B);
		public static readonly Color OliveGreen                = HexToColor(0xBAB86C);
		public static readonly Color Brass                     = HexToColor(0xB5A642);
		public static readonly Color CookieBrown               = HexToColor(0xC7A317);
		public static readonly Color MetallicGold              = HexToColor(0xD4AF37);
		public static readonly Color BeeYellow                 = HexToColor(0xE9AB17);
		public static readonly Color SchoolBusYellow           = HexToColor(0xE8A317);
		public static readonly Color GoldenRod                 = HexToColor(0xDAA520);
		public static readonly Color OrangeGold                = HexToColor(0xD4A017);
		public static readonly Color Caramel                   = HexToColor(0xC68E17);
		public static readonly Color DarkGoldenRod             = HexToColor(0xB8860B);
		public static readonly Color Cinnamon                  = HexToColor(0xC58917);
		public static readonly Color Peru                      = HexToColor(0xCD853F);
		public static readonly Color Bronze                    = HexToColor(0xCD7F32);
		public static readonly Color TigerOrange               = HexToColor(0xC88141);
		public static readonly Color Copper                    = HexToColor(0xB87333);
		public static readonly Color Wood                      = HexToColor(0x966F33);
		public static readonly Color OakBrown                  = HexToColor(0x806517);
		public static readonly Color AntiqueBronze             = HexToColor(0x665D1E);
		public static readonly Color Hazel                     = HexToColor(0x8E7618);
		public static readonly Color DarkYellow                = HexToColor(0x8B8000);
		public static readonly Color DarkMoccasin              = HexToColor(0x827839);
		public static readonly Color BulletShell               = HexToColor(0xAF9B60);
		public static readonly Color ArmyBrown                 = HexToColor(0x827B60);
		public static readonly Color Sandstone                 = HexToColor(0x786D5F);
		public static readonly Color Taupe                     = HexToColor(0x483C32);
		public static readonly Color Mocha                     = HexToColor(0x493D26);
		public static readonly Color MilkChocolate             = HexToColor(0x513B1C);
		public static readonly Color GrayBrown                 = HexToColor(0x3D3635);
		public static readonly Color DarkCoffee                = HexToColor(0x3B2F2F);
		public static readonly Color OldBurgundy               = HexToColor(0x43302E);
		public static readonly Color WesternCharcoal           = HexToColor(0x49413F);
		public static readonly Color BakersBrown               = HexToColor(0x5C3317);
		public static readonly Color DarkBrown                 = HexToColor(0x654321);
		public static readonly Color SepiaBrown                = HexToColor(0x704214);
		public static readonly Color Coffee                    = HexToColor(0x6F4E37);
		public static readonly Color BrownBear                 = HexToColor(0x835C3B);
		public static readonly Color RedDirt                   = HexToColor(0x7F5217);
		public static readonly Color Sepia                     = HexToColor(0x7F462C);
		public static readonly Color Sienna                    = HexToColor(0xA0522D);
		public static readonly Color SaddleBrown               = HexToColor(0x8B4513);
		public static readonly Color DarkSienna                = HexToColor(0x8A4117);
		public static readonly Color Sangria                   = HexToColor(0x7E3817);
		public static readonly Color BloodRed                  = HexToColor(0x7E3517);
		public static readonly Color Chestnut                  = HexToColor(0x954535);
		public static readonly Color ChestnutRed               = HexToColor(0xC34A2C);
		public static readonly Color Mahogany                  = HexToColor(0xC04000);
		public static readonly Color RedFox                    = HexToColor(0xC35817);
		public static readonly Color DarkBisque                = HexToColor(0xB86500);
		public static readonly Color LightBrown                = HexToColor(0xB5651D);
		public static readonly Color Rust                      = HexToColor(0xC36241);
		public static readonly Color CopperRed                 = HexToColor(0xCB6D51);
		public static readonly Color OrangeSalmon              = HexToColor(0xC47451);
		public static readonly Color Chocolate                 = HexToColor(0xD2691E);
		public static readonly Color Sedona                    = HexToColor(0xCC6600);
		public static readonly Color PapayaOrange              = HexToColor(0xE56717);
		public static readonly Color HalloweenOrange           = HexToColor(0xE66C2C);
		public static readonly Color NeonOrange                = HexToColor(0xFF6700);
		public static readonly Color BrightOrange              = HexToColor(0xFF5F1F);
		public static readonly Color PumpkinOrange             = HexToColor(0xF87217);
		public static readonly Color CarrotOrange              = HexToColor(0xF88017);
		public static readonly Color DarkOrange                = HexToColor(0xFF8C00);
		public static readonly Color ConstructionConeOrange    = HexToColor(0xF87431);
		public static readonly Color IndianSaffron             = HexToColor(0xFF7722);
		public static readonly Color SunriseOrange             = HexToColor(0xE67451);
		public static readonly Color MangoOrange               = HexToColor(0xFF8040);
		public static readonly Color Coral                     = HexToColor(0xFF7F50);
		public static readonly Color BasketBallOrange          = HexToColor(0xF88158);
		public static readonly Color LightSalmonRose           = HexToColor(0xF9966B);
		public static readonly Color LightSalmon               = HexToColor(0xFFA07A);
		public static readonly Color DarkSalmon                = HexToColor(0xE9967A);
		public static readonly Color Tangerine                 = HexToColor(0xE78A61);
		public static readonly Color LightCopper               = HexToColor(0xDA8A67);
		public static readonly Color Salmon                    = HexToColor(0xFA8072);
		public static readonly Color LightCoral                = HexToColor(0xF08080);
		public static readonly Color PastelRed                 = HexToColor(0xF67280);
		public static readonly Color PinkCoral                 = HexToColor(0xE77471);
		public static readonly Color BeanRed                   = HexToColor(0xF75D59);
		public static readonly Color ValentineRed              = HexToColor(0xE55451);
		public static readonly Color IndianRed                 = HexToColor(0xCD5C5C);
		public static readonly Color Tomato                    = HexToColor(0xFF6347);
		public static readonly Color ShockingOrange            = HexToColor(0xE55B3C);
		public static readonly Color OrangeRed                 = HexToColor(0xFF4500);
		public static readonly Color Red                       = HexToColor(0xFF0000);
		public static readonly Color NeonRed                   = HexToColor(0xFD1C03);
		public static readonly Color Scarlet                   = HexToColor(0xFF2400);
		public static readonly Color RubyRed                   = HexToColor(0xF62217);
		public static readonly Color FerrariRed                = HexToColor(0xF70D1A);
		public static readonly Color FireEngineRed             = HexToColor(0xF62817);
		public static readonly Color LavaRed                   = HexToColor(0xE42217);
		public static readonly Color LoveRed                   = HexToColor(0xE41B17);
		public static readonly Color Grapefruit                = HexToColor(0xDC381F);
		public static readonly Color CherryRed                 = HexToColor(0xC24641);
		public static readonly Color ChilliPepper              = HexToColor(0xC11B17);
		public static readonly Color FireBrick                 = HexToColor(0xB22222);
		public static readonly Color TomatoSauceRed            = HexToColor(0xB21807);
		public static readonly Color Brown                     = HexToColor(0xA52A2A);
		public static readonly Color CarbonRed                 = HexToColor(0xA70D2A);
		public static readonly Color Cranberry                 = HexToColor(0x9F000F);
		public static readonly Color SaffronRed                = HexToColor(0x931314);
		public static readonly Color RedWine                   = HexToColor(0x990012);
		public static readonly Color DarkRed                   = HexToColor(0x8B0000);
		public static readonly Color Maroon                    = HexToColor(0x800000);
		public static readonly Color Burgundy                  = HexToColor(0x8C001A);
		public static readonly Color DeepRed                   = HexToColor(0x800517);
		public static readonly Color RedBlood                  = HexToColor(0x660000);
		public static readonly Color BloodNight                = HexToColor(0x551606);
		public static readonly Color BlackBean                 = HexToColor(0x3D0C02);
		public static readonly Color ChocolateBrown            = HexToColor(0x3F000F);
		public static readonly Color Midnight                  = HexToColor(0x2B1B17);
		public static readonly Color PurpleLily                = HexToColor(0x550A35);
		public static readonly Color PurpleMaroon              = HexToColor(0x810541);
		public static readonly Color PlumPie                   = HexToColor(0x7D0541);
		public static readonly Color PlumVelvet                = HexToColor(0x7D0552);
		public static readonly Color DarkRaspberry             = HexToColor(0x872657);
		public static readonly Color VelvetMaroon              = HexToColor(0x7E354D);
		public static readonly Color RosyFinch                 = HexToColor(0x7F4E52);
		public static readonly Color DullPurple                = HexToColor(0x7F525D);
		public static readonly Color Puce                      = HexToColor(0x7F5A58);
		public static readonly Color RoseDust                  = HexToColor(0x997070);
		public static readonly Color RosyPink                  = HexToColor(0xB38481);
		public static readonly Color RosyBrown                 = HexToColor(0xBC8F8F);
		public static readonly Color KhakiRose                 = HexToColor(0xC5908E);
		public static readonly Color PinkBrown                 = HexToColor(0xC48189);
		public static readonly Color LipstickPink              = HexToColor(0xC48793);
		public static readonly Color Rose                      = HexToColor(0xE8ADAA);
		public static readonly Color SilverPink                = HexToColor(0xC4AEAD);
		public static readonly Color RoseGold                  = HexToColor(0xECC5C0);
		public static readonly Color DeepPeach                 = HexToColor(0xFFCBA4);
		public static readonly Color PastelOrange              = HexToColor(0xF8B88B);
		public static readonly Color DesertSand                = HexToColor(0xEDC9AF);
		public static readonly Color UnbleachedSilk            = HexToColor(0xFFDDCA);
		public static readonly Color PigPink                   = HexToColor(0xFDD7E4);
		public static readonly Color Blush                     = HexToColor(0xFFE6E8);
		public static readonly Color MistyRose                 = HexToColor(0xFFE4E1);
		public static readonly Color PinkBubbleGum             = HexToColor(0xFFDFDD);
		public static readonly Color LightRed                  = HexToColor(0xFFCCCB);
		public static readonly Color LightRose                 = HexToColor(0xFBCFCD);
		public static readonly Color DeepRose                  = HexToColor(0xFBBBB9);
		public static readonly Color Pink                      = HexToColor(0xFFC0CB);
		public static readonly Color LightPink                 = HexToColor(0xFFB6C1);
		public static readonly Color DonutPink                 = HexToColor(0xFAAFBE);
		public static readonly Color BabyPink                  = HexToColor(0xFAAFBA);
		public static readonly Color FlamingoPink              = HexToColor(0xF9A7B0);
		public static readonly Color PastelPink                = HexToColor(0xFEA3AA);
		public static readonly Color PinkRose                  = HexToColor(0xE7A1B0);
		public static readonly Color PinkDaisy                 = HexToColor(0xE799A3);
		public static readonly Color CadillacPink              = HexToColor(0xE38AAE);
		public static readonly Color CarnationPink             = HexToColor(0xF778A1);
		public static readonly Color BlushRed                  = HexToColor(0xE56E94);
		public static readonly Color PaleVioletRed             = HexToColor(0xDB7093);
		public static readonly Color PurplePink                = HexToColor(0xD16587);
		public static readonly Color TulipPink                 = HexToColor(0xC25A7C);
		public static readonly Color BashfulPink               = HexToColor(0xC25283);
		public static readonly Color DarkPink                  = HexToColor(0xE75480);
		public static readonly Color DarkHotPink               = HexToColor(0xF660AB);
		public static readonly Color HotPink                   = HexToColor(0xFF69B4);
		public static readonly Color WatermelonPink            = HexToColor(0xFC6C85);
		public static readonly Color VioletRed                 = HexToColor(0xF6358A);
		public static readonly Color HotDeepPink               = HexToColor(0xF52887);
		public static readonly Color DeepPink                  = HexToColor(0xFF1493);
		public static readonly Color NeonPink                  = HexToColor(0xF535AA);
		public static readonly Color NeonHotPink               = HexToColor(0xFD349C);
		public static readonly Color PinkCupcake               = HexToColor(0xE45E9D);
		public static readonly Color DimorphothecaMagenta      = HexToColor(0xE3319D);
		public static readonly Color PinkLemonade              = HexToColor(0xE4287C);
		public static readonly Color Raspberry                 = HexToColor(0xE30B5D);
		public static readonly Color Crimson                   = HexToColor(0xDC143C);
		public static readonly Color BrightMaroon              = HexToColor(0xC32148);
		public static readonly Color RoseRed                   = HexToColor(0xC21E56);
		public static readonly Color RoguePink                 = HexToColor(0xC12869);
		public static readonly Color BurntPink                 = HexToColor(0xC12267);
		public static readonly Color PinkViolet                = HexToColor(0xCA226B);
		public static readonly Color MediumVioletRed           = HexToColor(0xC71585);
		public static readonly Color DarkCarnationPink         = HexToColor(0xC12283);
		public static readonly Color RaspberryPurple           = HexToColor(0xB3446C);
		public static readonly Color PinkPlum                  = HexToColor(0xB93B8F);
		public static readonly Color Orchid                    = HexToColor(0xDA70D6);
		public static readonly Color DeepMauve                 = HexToColor(0xDF73D4);
		public static readonly Color Violet                    = HexToColor(0xEE82EE);
		public static readonly Color BrightNeonPink            = HexToColor(0xF433FF);
		public static readonly Color Magenta                   = HexToColor(0xFF00FF);
		public static readonly Color CrimsonPurple             = HexToColor(0xE238EC);
		public static readonly Color HeliotropePurple          = HexToColor(0xD462FF);
		public static readonly Color TyrianPurple              = HexToColor(0xC45AEC);
		public static readonly Color MediumOrchid              = HexToColor(0xBA55D3);
		public static readonly Color PurpleFlower              = HexToColor(0xA74AC7);
		public static readonly Color OrchidPurple              = HexToColor(0xB048B5);
		public static readonly Color PastelViolet              = HexToColor(0xD291BC);
		public static readonly Color MauveTaupe                = HexToColor(0x915F6D);
		public static readonly Color ViolaPurple               = HexToColor(0x7E587E);
		public static readonly Color Eggplant                  = HexToColor(0x614051);
		public static readonly Color PlumPurple                = HexToColor(0x583759);
		public static readonly Color Grape                     = HexToColor(0x5E5A80);
		public static readonly Color PurpleNavy                = HexToColor(0x4E5180);
		public static readonly Color SlateBlue                 = HexToColor(0x6A5ACD);
		public static readonly Color BlueLotus                 = HexToColor(0x6960EC);
		public static readonly Color LightSlateBlue            = HexToColor(0x736AFF);
		public static readonly Color MediumSlateBlue           = HexToColor(0x7B68EE);
		public static readonly Color PeriwinklePurple          = HexToColor(0x7575CF);
		public static readonly Color PurpleAmethyst            = HexToColor(0x6C2DC7);
		public static readonly Color BrightPurple              = HexToColor(0x6A0DAD);
		public static readonly Color DeepPeriwinkle            = HexToColor(0x5453A6);
		public static readonly Color DarkSlateBlue             = HexToColor(0x483D8B);
		public static readonly Color PurpleHaze                = HexToColor(0x4E387E);
		public static readonly Color PurpleIris                = HexToColor(0x571B7E);
		public static readonly Color DarkPurple                = HexToColor(0x4B0150);
		public static readonly Color DeepPurple                = HexToColor(0x36013F);
		public static readonly Color PurpleMonster             = HexToColor(0x461B7E);
		public static readonly Color Indigo                    = HexToColor(0x4B0082);
		public static readonly Color BlueWhale                 = HexToColor(0x342D7E);
		public static readonly Color RebeccaPurple             = HexToColor(0x663399);
		public static readonly Color PurpleJam                 = HexToColor(0x6A287E);
		public static readonly Color DarkMagenta               = HexToColor(0x8B008B);
		public static readonly Color Purple                    = HexToColor(0x800080);
		public static readonly Color FrenchLilac               = HexToColor(0x86608E);
		public static readonly Color DarkOrchid                = HexToColor(0x9932CC);
		public static readonly Color DarkViolet                = HexToColor(0x9400D3);
		public static readonly Color PurpleViolet              = HexToColor(0x8D38C9);
		public static readonly Color JasminePurple             = HexToColor(0xA23BEC);
		public static readonly Color PurpleDaffodil            = HexToColor(0xB041FF);
		public static readonly Color ClemantisViolet           = HexToColor(0x842DCE);
		public static readonly Color BlueViolet                = HexToColor(0x8A2BE2);
		public static readonly Color PurpleSageBush            = HexToColor(0x7A5DC7);
		public static readonly Color LovelyPurple              = HexToColor(0x7F38EC);
		public static readonly Color NeonPurple                = HexToColor(0x9D00FF);
		public static readonly Color PurplePlum                = HexToColor(0x8E35EF);
		public static readonly Color AztechPurple              = HexToColor(0x893BFF);
		public static readonly Color LavenderPurple            = HexToColor(0x967BB6);
		public static readonly Color MediumPurple              = HexToColor(0x9370DB);
		public static readonly Color LightPurple               = HexToColor(0x8467D7);
		public static readonly Color CrocusPurple              = HexToColor(0x9172EC);
		public static readonly Color PurpleMimosa              = HexToColor(0x9E7BFF);
		public static readonly Color Periwinkle                = HexToColor(0xCCCCFF);
		public static readonly Color PaleLilac                 = HexToColor(0xDCD0FF);
		public static readonly Color Mauve                     = HexToColor(0xE0B0FF);
		public static readonly Color BrightLilac               = HexToColor(0xD891EF);
		public static readonly Color RichLilac                 = HexToColor(0xB666D2);
		public static readonly Color PurpleDragon              = HexToColor(0xC38EC7);
		public static readonly Color Lilac                     = HexToColor(0xC8A2C8);
		public static readonly Color Plum                      = HexToColor(0xDDA0DD);
		public static readonly Color BlushPink                 = HexToColor(0xE6A9EC);
		public static readonly Color PastelPurple              = HexToColor(0xF2A2E8);
		public static readonly Color BlossomPink               = HexToColor(0xF9B7FF);
		public static readonly Color WisteriaPurple            = HexToColor(0xC6AEC7);
		public static readonly Color PurpleThistle             = HexToColor(0xD2B9D3);
		public static readonly Color Thistle                   = HexToColor(0xD8BFD8);
		public static readonly Color PeriwinklePink            = HexToColor(0xE9CFEC);
		public static readonly Color CottonCandy               = HexToColor(0xFCDFFF);
		public static readonly Color LavenderPinocchio         = HexToColor(0xEBDDE2);
		public static readonly Color AshWhite                  = HexToColor(0xE9E4D4);
		public static readonly Color WhiteChocolate            = HexToColor(0xEDE6D6);
		public static readonly Color SoftIvory                 = HexToColor(0xFAF0DD);
		public static readonly Color OffWhite                  = HexToColor(0xF8F0E3);
		public static readonly Color LavenderBlush             = HexToColor(0xFFF0F5);
		public static readonly Color Pearl                     = HexToColor(0xFDEEF4);
		public static readonly Color EggShell                  = HexToColor(0xFFF9E3);
		public static readonly Color OldLace                   = HexToColor(0xFDF5E6);
		public static readonly Color Linen                     = HexToColor(0xFAF0E6);
		public static readonly Color SeaShell                  = HexToColor(0xFFF5EE);
		public static readonly Color Rice                      = HexToColor(0xFAF5EF);
		public static readonly Color FloralWhite               = HexToColor(0xFFFAF0);
		public static readonly Color Ivory                     = HexToColor(0xFFFFF0);
		public static readonly Color LightWhite                = HexToColor(0xFFFFF7);
		public static readonly Color WhiteSmoke                = HexToColor(0xF5F5F5);
		public static readonly Color Cotton                    = HexToColor(0xFBFBF9);
		public static readonly Color Snow                      = HexToColor(0xFFFAFA);
		public static readonly Color MilkWhite                 = HexToColor(0xFEFCFF);
		public static readonly Color White                     = HexToColor(0xFFFFFF);

        #endregion

        public Color(float n)
        {
            (R, G, B, A) = (n, n, n, n);
        }

        public Color(float r, float g, float b, float a)
        {
            (R, G, B, A) = (r, g, b, a);
        }

		public float this[int index]
		{
			get
			{
				if (index == 0)
					return R;
				else if (index == 1)
					return G;
				else if (index == 2)
					return B;
				else if (index == 3)
					return A;
				throw new IndexOutOfRangeException($"index of {index} is out of range of Color(RGBA)!");
			}
			set
			{
				if (index == 0)
					R = value;
				else if (index == 1)
					G = value;
				else if (index == 2)
					B = value;
				else if (index == 3)
					A = value;
				throw new IndexOutOfRangeException($"index of {index} is out of range of Color(RGBA)!");
			}
		}

		public static Color HexToColor(int hex)
		{
			byte[] bytes = new byte[3];
			for(int i = 0; i <= 2; i++)
			{
				bytes[i] = (byte)((hex >> (8 * i)) & 0xff);
			}
			return new Color(bytes[2]/255f, bytes[1]/255f, bytes[0]/255f, 1.0f);
		}

        #region COLOR_OPERATORS
        public static Color operator +(Color col1, Color col2)
        {
            float r = col1.R + col2.R;
            float g = col1.G + col2.G;
            float b = col1.B + col2.B;
            float a = col1.A + col2.A;
            return new Color() { R = r, G = g, B = b, A = a};
        }

        public static Color operator -(Color col1, Color col2)
        {
            float r = col1.R - col2.R;
            float g = col1.G - col2.G;
            float b = col1.B - col2.B;
            float a = col1.A - col2.A;
            return new Color() { R = r, G = g, B = b, A = a };
        }

        public static Color operator *(Color col1, Color col2)
        {
            float r = col1.R * col2.R;
            float g = col1.G * col2.G;
            float b = col1.B * col2.B;
            float a = col1.A * col2.A;
            return new Color() { R = r, G = g, B = b, A = a };
        }

        public static Color operator /(Color col1, Color col2)
        {
            float r = col1.R / col2.R;
            float g = col1.G / col2.G;
            float b = col1.B / col2.B;
            float a = col1.A / col2.A;
            return new Color() { R = r, G = g, B = b, A = a };
        }
        #endregion

        #region SCALAR_OPERATORS
        public static Color operator +(Color col, float scalar)
        {
            Color other = new Color(scalar);
            return col + other;
        }

        public static Color operator -(Color col, float scalar)
        {
            Color other = new Color(scalar);
            return col - other;
        }

        public static Color operator *(Color col, float scalar)
        {
            Color other = new Color(scalar);
            return col * other;
        }

        public static Color operator /(Color col, float scalar)
        {
            Color other = new Color(scalar);
            return col / other;
        }
        public static Color operator +(float scalar, Color col)
        {
            Color other = new Color(scalar);
            return col + other;
        }

        public static Color operator -(float scalar, Color col)
        {
            Color other = new Color(scalar);
            return col - other;
        }

        public static Color operator *(float scalar, Color col)
        {
            Color other = new Color(scalar);
            return col * other;
        }

        public static Color operator /(float scalar, Color col)
        {
            Color other = new Color(scalar);
            return col / other;
        }
        #endregion

        public bool Equals(Color other)
        {
            return (R == other.R && G == other.G && B == other.B && A == other.A);
        }
    }
}
