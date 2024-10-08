# pwpa-call-logging
 The call logging component of the Police Workforce Planner & Analytics Solution

## How to run?
1. Configure the VM in bridge mode
2. Clone the repository inside a server
3. Update `DEBUG.ENV` with values
4. Run `docker compose --env-file DEBUG.env -f docker-compose-dev.yml up -d`
5. Access https://server.ip:8004
