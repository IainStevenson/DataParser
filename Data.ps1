while(1)
{

	.\speedtest --format=json-pretty > "$env:AppData\Ookla\Data\$(((get-date).ToUniversalTime()).ToString("yyyy-MM-dd-T-HH-mm-ss-ffffff")).json"
	start-sleep -seconds 1200
}

