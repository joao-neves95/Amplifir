﻿
-- TODO: Optimize data types.
-- TODO: Add CHECKs.

SET TIME ZONE 'UTC';

CREATE TABLE AppUser (
    Id SERIAL PRIMARY KEY,
    UserName VARCHAR(45) NOT NULL,
    -- Max email length RFC3696 Errata ID 1690.
    Email VARCHAR(254) NOT NULL,
    Password VARCHAR(255) NOT NULL,
    PhoneNumber VARCHAR(50) NULL,
    PhoneNumberConfirmed BOOLEAN NOT NULL DEFAULT FALSE,
    EmailConfirmed BOOLEAN NOT NULL DEFAULT FALSE,
    TwoFactorEnabled BOOLEAN NOT NULL DEFAULT FALSE,
    FailedLoginCount SMALLINT NOT NULL DEFAULT 0,
    CreateDate TIMESTAMP WITHOUT TIMEZONE NOT NULL DEFAULT (NOW() AT TIME ZONE 'UTC')
);

CREATE TABLE AppUserProfile (
    Id SERIAL PRIMARY KEY,
    UserId INT NOT NULL References AppUser(Id),
    Bio VARCHAR(200) NOT NULL DEFAULT '',
    FollowingCount INT NOT NULL DEFAULT 0,
    FollowersCount INT NOT NULL DEFAULT 0,
    UserLocation VARCHAR(45) NOT NULL DEFAULT '',
    BirthDate TIMESTAMP WITHOUT TIMEZONE
);

CREATE TABLE Follower (
    Id SERIAL PRIMARY KEY,
    UserId INT NOT NULL REFERENCES AppUser(Id),
    FollowerId INT NOT NULL REFERENCES AppUser(Id)
);

CREATE TABLE Shout (
    Id SERIAL PRIMARY KEY,
    UserId INT NOT NULL REFERENCES AppUser(Id),
    Content VARCHAR(90) NOT NULL,
    CreateDate TIMESTAMP WITHOUT TIMEZONE NOT NULL DEFAULT (NOW() AT TIME ZONE 'UTC'),
    UpdateDate TIMESTAMP WITHOUT TIMEZONE NOT NULL DEFAULT (NOW() AT TIME ZONE 'UTC'),
    LikesNum INT NOT NULL DEFAULT 0,
    DislikesNum INT NOT NULL DEFAULT 0
);

CREATE TABLE ShoutHashtag (
    Id SERIAL PRIMARY KEY,
    ShoutId INT NOT NULL REFERENCES Shout(Id),
    HashtagId BIGINT NOT NULL REFERENCES Hashtag(Id)
);

CREATE TABLE Hashtag (
    Id BIGSERIAL PRIMARY KEY,
    Label VARCHAR(45) NOT NULL
);

CREATE TABLE ShoutAsset (
    Id SERIAL PRIMARY KEY,
    ShoutIdId INT NOT NULL REFERENCES Shout(Id),
    AssetTypeId SMALLINT NOT NULL,
    URL VARCHAR(100) NOT NULL
);

CREATE TABLE AssetType (
    Id SMALLSERIAL PRIMARY KEY,
    Label VARCHAR(20) NOT NULL,
);

CREATE TABLE ShoutReaction (
    Id SERIAL PRIMARY KEY,
    UserId INT NOT NULL REFERENCES AppUser(Id),
    ShoutId INT NOT NULL REFERENCES Shout(Id),
    ReactionTypeId SMALLINT NOT NULL REFERENCES ReactionType(Id)
);

CREATE TABLE Comment (
    Id SERIAL PRIMARY KEY,
    UserId INT NOT NULL REFERENCES AppUser(Id),
    ShoutId INT NOT NULL REFERENCES Shout(Id),
    Content VARCHAR(90) NOT NULL,
    CreateDate TIMESTAMP WITHOUT TIMEZONE NOT NULL DEFAULT (NOW() AT TIME ZONE 'UTC'),
    UpdateDate TIMESTAMP WITHOUT TIMEZONE NOT NULL DEFAULT (NOW() AT TIME ZONE 'UTC')
);

CREATE TABLE CommentReaction (
    Id SERIAL PRIMARY KEY,
    UserId INT NOT NULL REFERENCES AppUser(Id),
    CommentId INT NOT NULL REFERENCES Comment(Id),
    ReactionType SMALLINT NOT NULL REFERENCES ReactionType(Id)
);

CREATE TABLE ReactionType (
    Id SMALLSERIAL PRIMARY KEY,
    Label VARCHAR(30) NOT NULL,
);

CREATE TABLE Notification (
    Id SERIAL PRIMARY KEY,
    UserId INT NOT NULL REFERENCES AppUser(Id),
    NotificationTypeId SMALLINT NOT NULL REFERENCES NotificationType(Id),
    -- Relative link.
    LinksTo VARCHAR(100),
    CreateDate TIMESTAMP WITHOUT TIMEZONE NOT NULL DEFAULT (NOW() AT TIME ZONE 'UTC')
);

CREATE TABLE NotificationType (
    Id SMALLSERIAL PRIMARY KEY,
    Label VARCHAR(50) NOT NULL,
);

CREATE TABLE AuditLog (
    Id SERIAL PRIMARY KEY,
    UserId INT REFERENCES AppUser(Id),
    IPv4 VARCHAR(15),
    EventType SMALLINT NOT NULL,
    CreateDate TIMESTAMP WITHOUT TIMEZONE NOT NULL DEFAULT (NOW() AT TIME ZONE 'UTC')
);

CREATE TABLE EventType (
    Id SMALLSERIAL PRIMARY KEY,
    Label VARCHAR(50) NOT NULL,
);
