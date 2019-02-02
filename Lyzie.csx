					EnsureDataLoaded();

					// a horribe combinment of EnableDebug, DebugToggler, and dogcheck fucking code; i originally do a program for this, but it's output was entire broken
					
                    ScriptMessage("a horribe combinment of EnableDebug, DebugToggler, and dogcheck fucking code. by LWYS");
					
					var ok = false;
                    var SCR_GAMESTART = Data.Scripts.ByName("SCR_GAMESTART")?.Code;
                    var toggle = Data.GameObjects.ByName("obj_time").EventHandlerFor(EventType.KeyPress, EventSubtypeKey.vk_f12, Data.Strings, Data.Code, Data.CodeLocals);
                    var scr_dogcheck = Data.Scripts.ByName("scr_dogcheck")?.Code;
                    var scr_debug = Data.Scripts.ByName("scr_debug")?.Code;

                    // debug enable part
                    if (scr_debug != null) // Deltarune
                    {
                        ScriptMessage("Deltarune Detected!");
                        scr_debug.Replace(Assembler.Assemble(@"
pushglb.v global.debug
ret.v
", Data));
                    }
                    if (SCR_GAMESTART == null)
                        SCR_GAMESTART = Data.Scripts.ByName("scr_gamestart")?.Code; // Deltarune
                    if (SCR_GAMESTART == null)
                        throw new Exception("Script SCR_GAMESTART not found");
                    for (int i = 0; i < SCR_GAMESTART.Instructions.Count; i++)
                    {
                        if (SCR_GAMESTART.Instructions[i].Kind == UndertaleInstruction.Opcode.Pop && SCR_GAMESTART.Instructions[i].Destination.Target.Name.Content == "debug")
                        {
                            ok = true;
                            bool enable = ScriptQuestion("Enable or disable debug mode?");
                            SCR_GAMESTART.Instructions[i - 1].Value = (short)(enable ? 1 : 0);
                            ScriptMessage("Debug mode " + (enable ? "enabled" : "disabled"));
                        }
                    }
                    if (!ok)
                        throw new Exception("Debug patch point not found???");

                    // toggle key
                    toggle.Append(Assembler.Assemble(@"
pushglb.v global.debug
pushi.e 1
cmp.i.v EQ
bf go_enable

pushi.e 0
pop.v.i global.debug
b func_end

go_enable: pushi.e 1
pop.v.i global.debug
", Data));

                    // dogcheck f**ker
                    if (scr_debug != null)
                    { //Deltarune
                        scr_dogcheck.Replace(Assembler.Assemble(@"
pushi.e 0
conv.i.v
ret.v
", Data));
                    }
                    else
                    {
                        scr_dogcheck.Replace(Assembler.Assemble(@"
pushi.e 0
pop.v.i self.wck
", Data));
                    }
					ScriptMessage("ok; 4urdata: debug enabled; f12 to toggle debug; dogcheck disabled");
