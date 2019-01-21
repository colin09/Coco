db.createUser(
    {
        user: "root",
        pwd: "password",
        roles:[
            {
                role: "userAdminAnyDatabase",
                db: "admin"
            }
        ]
    }
);