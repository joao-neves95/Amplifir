/*
 * Copyright (c) 2019 - 2020 João Pedro Martins Neves (SHIVAYL) - All Rights Reserved.
 *
 * Amplifir and all its content is licensed under the GNU Lesser General Public License (LGPL),
 * version 3, located in the root of this project, under the name "LICENSE.md".
 *
 */


SET TIME ZONE 'UTC';

CREATE TABLE AppUser (
    Id SERIAL PRIMARY KEY,
    UserName VARCHAR(45) UNIQUE NOT NULL,
    -- Max email length RFC3696 Errata ID 1690.
    Email VARCHAR(254) UNIQUE NOT NULL,
    Password VARCHAR(255) NOT NULL,
    PhoneNumber VARCHAR(50) UNIQUE NULL,
    PhoneNumberConfirmed BOOLEAN NOT NULL DEFAULT FALSE,
    EmailConfirmed BOOLEAN NOT NULL DEFAULT FALSE,
    TwoFactorEnabled BOOLEAN NOT NULL DEFAULT FALSE,
    FailedLoginCount SMALLINT NOT NULL DEFAULT 0,
    LockoutEnabled BOOLEAN NOT NULL DEFAULT FALSE,
    CreateDate TIMESTAMP WITHOUT TIME ZONE NOT NULL DEFAULT( NOW() AT TIME ZONE 'UTC' )
);

CREATE TABLE AppUserProfile (
    Id SERIAL PRIMARY KEY,
    UserId INT UNIQUE NOT NULL References AppUser(Id),
    Bio VARCHAR(250) NULL DEFAULT NULL CHECK( Bio = NULL OR LENGTH( Bio ) > 0 ),
    Website VARCHAR(254) NULL DEFAULT NULL CHECK( Website = NULL OR LENGTH( Website ) > 0 ),
    UserLocation VARCHAR(45) NULL DEFAULT NULL CHECK( UserLocation = NULL OR LENGTH( UserLocation ) > 1 ),
    BirthDate TIMESTAMP WITHOUT TIME ZONE NULL DEFAULT NULL CHECK( BirthDate = NULL OR BirthDate < (NOW() AT TIME ZONE 'UTC') ),
    FollowingCount INT NOT NULL DEFAULT 0,
    FollowersCount INT NOT NULL DEFAULT 0
);

CREATE TABLE Follower (
    Id SERIAL PRIMARY KEY,
    UserId INT NOT NULL REFERENCES AppUser(Id),
    FollowerId INT NOT NULL REFERENCES AppUser(Id)
);

CREATE TABLE Shout (
    Id SERIAL PRIMARY KEY,
    UserId INT NOT NULL REFERENCES AppUser(Id),
    Content VARCHAR(90) NOT NULL CHECK( LENGTH( Content ) > 0 ),
    CreateDate TIMESTAMP WITHOUT TIME ZONE NOT NULL DEFAULT( NOW() AT TIME ZONE 'UTC' ),
    LikesCount INT NOT NULL DEFAULT 0,
    DislikesCount INT NOT NULL DEFAULT 0
);

CREATE TABLE Hashtag (
    Id BIGSERIAL PRIMARY KEY,
    Content VARCHAR(69) UNIQUE NOT NULL CHECK( LENGTH( Content ) > 0 ),
    ShoutCount INT NOT NULL DEFAULT 0
);

CREATE TABLE HashtagShout (
    Id SERIAL PRIMARY KEY,
    HashtagId BIGINT NOT NULL REFERENCES Hashtag(Id),
    ShoutId INT NOT NULL REFERENCES Shout(Id)
);

CREATE TABLE ShoutAsset (
    Id SERIAL PRIMARY KEY,
    ShoutId INT NOT NULL REFERENCES Shout(Id),
    AssetTypeId SMALLINT NOT NULL,
    URL VARCHAR(200) NOT NULL
);

CREATE TABLE AssetType (
    Id SMALLSERIAL PRIMARY KEY,
    Label VARCHAR(20) NOT NULL,
);

CREATE TABLE ReactionType (
    Id SMALLSERIAL PRIMARY KEY,
    Label VARCHAR(30) NOT NULL,
);

CREATE TABLE ShoutReaction (
    Id SERIAL PRIMARY KEY,
    ShoutId INT NOT NULL REFERENCES Shout(Id),
    UserId INT NOT NULL REFERENCES AppUser(Id),
    ReactionTypeId SMALLINT NOT NULL REFERENCES ReactionType(Id)
);

CREATE TABLE Comment (
    Id SERIAL PRIMARY KEY,
    ShoutId INT NOT NULL REFERENCES Shout(Id),
    UserId INT NOT NULL REFERENCES AppUser(Id),
    Content VARCHAR(90) NOT NULL CHECK( LENGTH( Content ) > 0 ),
    CreateDate TIMESTAMP WITHOUT TIME ZONE NOT NULL DEFAULT( NOW() AT TIME ZONE 'UTC' ),
    LikesCount INT NOT NULL DEFAULT 0,
    DislikesCount INT NOT NULL DEFAULT 0
);

CREATE TABLE CommentReaction (
    Id SERIAL PRIMARY KEY,
    CommentId INT NOT NULL REFERENCES Comment(Id),
    UserId INT NOT NULL REFERENCES AppUser(Id),
    ReactionType SMALLINT NOT NULL REFERENCES ReactionType(Id)
);

CREATE TABLE Notification (
    Id SERIAL PRIMARY KEY,
    UserId INT NOT NULL REFERENCES AppUser(Id),
    NotificationTypeId SMALLINT NOT NULL REFERENCES NotificationType(Id),
    Content VARCHAR(200) NULL,
    -- Relative link.
    LinksTo VARCHAR(100),
    CreateDate TIMESTAMP WITHOUT TIME ZONE NOT NULL DEFAULT( NOW() AT TIME ZONE 'UTC' )
);

CREATE TABLE NotificationType (
    Id SMALLSERIAL PRIMARY KEY,
    Label VARCHAR(50) NOT NULL,
);

CREATE TABLE AuditLog (
    Id SERIAL PRIMARY KEY,
    UserId INT REFERENCES AppUser(Id),
    IPv4 VARCHAR(15) CHECK( LENGTH( IPv4 ) >= 7 ),
    EventTypeId SMALLINT NOT NULL,
    CreateDate TIMESTAMP WITHOUT TIME ZONE NOT NULL DEFAULT( NOW() AT TIME ZONE 'UTC' )
);

CREATE TABLE EventType (
    Id SMALLSERIAL PRIMARY KEY,
    Label VARCHAR(50) NOT NULL
);
