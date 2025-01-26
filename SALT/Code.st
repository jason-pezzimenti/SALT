[Template] Greeter {
	local string name;

	out with "Hello, what's your name?", expect response {
		name: response;
	}
}