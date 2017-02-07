-- http://198.211.121.98:8000/api/data?from={0}&count={1}
-- {"alpha":[6,0,0,0,0,0,0,5,0,0],"beta":[0,0,0,0,5,3,0,0,0,0],"gamma":[1,0,7,0,0,0,0,0,0,0],"delta":[3,0,4,0,6,0,3,0,0,0],"epsilon":[0,0,0,2,4,0,0,0,2,8]}

local http = require("socket.http")

local DELAY = 0.5
local FORM_RADIUS = 10
local FORM_SPEED = 30
local FORM_LIFETIME = 10

local data
local from = 0
local count = 10
local currentIndex = 1
local currentTime = 0
local forms = {}

function updateData()
	local txtData = http.request("http://198.211.121.98:8000/api/data?from=" .. from .. "&count=" .. count)
	data = parseData(txtData)

	-- print(data)
	-- for k in pairs(data) do
	--  	print(k .. ": ")
	--  	for i, v in ipairs(data[k]) do
	--  		print('    - [' .. i .. '] = ' .. v)
	--  	end
	-- end

	from = from + count
end

function parseData(data)
	local replaced = "return " .. string.gsub(data, '%"(%w+)%":%[([%d,]*)%]', "%1={%2}")
	return loadstring(replaced)()
end

function getColorFromName(name)
	local color = {0, 0, 0, 255}
	for i = 1, # name do
		color[i % 3 + 1] = (color[i % 3 + 1] + string.byte(name, i)) % 256
	end

	return color
end

function createForm(size, color)
	if size == 0 then
		return
	end

	local width, height = love.window.getMode()
	local new = {}
	new.color = color
	new.radius = size * FORM_RADIUS
	new.x = love.math.random(new.radius, width * 0.5 - new.radius)
	new.y = love.math.random(new.radius, height - new.radius)
	new.lifetime = FORM_LIFETIME
	table.insert(forms, new)
end

function updateForm(index, dt)
	forms[index].x = forms[index].x + FORM_SPEED * dt
	forms[index].color[4] = forms[index].color[4] - 255 / FORM_LIFETIME * dt
	forms[index].lifetime = forms[index].lifetime - dt

	if forms[index].lifetime < 0 then
		table.remove(forms, index)
	end
end

function drawForm(index)
	love.graphics.setColor(forms[index].color)
	love.graphics.circle("fill", forms[index].x, forms[index].y, forms[index].radius)
end

-- LOVE CALLBACKS --

function love.load()
	love.graphics.setBackgroundColor(30, 30, 30)
	love.math.setRandomSeed(os.time())
	updateData()
end

function love.keypressed(pkey)
	if pkey == 'escape' then
		love.event.quit()
		return
	end
end

function  love.update(dt)
	-- go in reverse to avoid index error when a form dies
	for i = # forms, 1, -1 do
		updateForm(i, dt)
	end

	if data ~= nil and currentIndex > count then
		updateData()
		currentIndex = 1
		return
	end

	currentTime = currentTime + dt
	if currentTime >= DELAY then
		for key in pairs(data) do
			createForm(data[key][currentIndex], getColorFromName(key))
		end
	
		currentIndex = currentIndex + 1
		currentTime = 0
	end
end

function love.draw()
	for i = 1, # forms do
		drawForm(i)
	end
end
