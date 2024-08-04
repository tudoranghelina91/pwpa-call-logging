db.createUser(
    {
        user: "root",
        pwd: "password",
        roles: [
            {
                role: "readWrite",
                db: "pwpa-call-logging-db"
            }
        ]
    }
);
db.createCollection('Calls');