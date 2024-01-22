toggleMenu = 0

hook.Add('OnGUI','GuiWindow',function()
    -- GUI functions can be found on https://docs.unity3d.com/ScriptReference/GUI.html
	
	if toggleMenu > 0 then
		GUI.Window(0, Rect(20,20,700,400), function()
			GUI.skin.label.fontSize = 18
			GUI.Label(Rect(25,20,695,100),'This is an experimental feature to bring Lua based mods to the FPV.SkyDive community')

			GUI.color = Color.red
			GUI.skin.label.fontSize = 25
			GUI.Label(Rect(25,60,695,100),'Be aware of malicious Lua files. Only install mods from trusted sources.')

			GUI.color = Color.white
			GUI.skin.label.fontSize = 18

			if GUI.Button(Rect(595,375,100,20),'Close') then
				print('Hiding mod manager GUI')
				toggleMenu = 0
			end
		end, "Mods Manager")
	end
    
end)

hook.Add('OnUpdate','KeyDownManager',function()
    if InputManager.GetKeyDown(KeyCode.F1) then
        print("F1 key pressed. Toggle mod menu")
		if toggleMenu > 0 then
			toggleMenu = 0
		else
			toggleMenu = 1
		end
	
    end
end)


hook.Add('OnApplicationStart','OnApplicationStart',function()
    print("### OnApplicationStart ###")
end)


hook.Add('LoadScene','LoadScene',function()
    print(">> LoadScene Lua hook called from Harmony patch")
end)


