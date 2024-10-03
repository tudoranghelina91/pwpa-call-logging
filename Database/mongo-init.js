const rootUser = process.env.MONGO_INITDB_ROOT_USERNAME;
const rootPassword = process.env.MONGO_INITDB_ROOT_PASSWORD;
const database = process.env.MONGO_INITDB_DATABASE;

db.createUser(
    {
        user: rootUser,
        pwd: rootPassword,
        roles: [
            {
                role: "readWrite",
                db: database
            }
        ]
    }
);
db.createCollection('Calls');