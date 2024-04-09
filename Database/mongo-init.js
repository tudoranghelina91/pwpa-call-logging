db.createUser(
    {
        user: "#{username}#",
        pwd: "#{password}#",
        roles: [
            {
                role: "readWrite",
                db: "pwpa-call-logging-db"
            }
        ]
    }
);
db.createCollection('Calls');