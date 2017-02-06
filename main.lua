-- http://198.211.121.98:8000/api/data?from={0}&count={1}
-- {"alpha":[6,0,0,0,0,0,0,5,0,0],"beta":[0,0,0,0,5,3,0,0,0,0],"gamma":[1,0,7,0,0,0,0,0,0,0],"delta":[3,0,4,0,6,0,3,0,0,0],"epsilon":[0,0,0,2,4,0,0,0,2,8]}

local http = require("socket.http")

local data
local from = 0
local count = 10

function love.load()
	love.graphics.setBackgroundColor(30, 30, 30)
	updateData(from, count)
	from = from + count
end

function love.keypressed(pkey)
	if pkey == 'escape' then
		love.event.quit()
		return
	end
end

function updateData(from, count)
	local txtData = http.request("http://198.211.121.98:8000/api/data?from=" .. from .. "&count=" .. count)
	print(txtData)
	local data = parseData(txtData)
	print(data)
end

function parseData(data)
	local replaced = string.gsub(data, '%"(%w+)%":%[([%d,]*)%]', "%1={%2}")
	print(replaced)
	return loadstring(replaced)
end
