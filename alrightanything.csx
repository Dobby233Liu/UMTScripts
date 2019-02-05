var anythingfile=Data.Sounds.ByName("snd_joker_anything")?.AudioFile;
var anythingfileja=Data.Sounds.ByName("snd_joker_anything_ja")?.AudioFile;
var walkinglist={
"chaos",
"byebye",
"metamorphosis",
"neochaos"
}
for(var zz=0;zz<walkinglist.Length-1;zz++){
Data.Sounds.ByName("snd_joker_"+walkinglist[zz])?.AudioFile=anythingfile
Data.Sounds.ByName("snd_joker_"+walkinglist[zz]+"_ja")?.AudioFile=anythingfileja
}
ScriptMessage("Do anything except chaos\nPatched");