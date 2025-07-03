CREATE TABLE Users (
  user_id         TEXT PRIMARY KEY,
  username        TEXT NOT NULL,                    
  email           TEXT NOT NULL UNIQUE,
  password_hash   TEXT NOT NULL,
  role            TEXT NOT NULL DEFAULT 'user',             -- user | moderator | admin
  created_at      TEXT NOT NULL DEFAULT (datetime('now'))
);

CREATE TABLE Topics (
  topic_id        TEXT PRIMARY KEY,
  name            TEXT UNIQUE NOT NULL,
  description     TEXT
);

CREATE TABLE Predictions (
  prediction_id     TEXT PRIMARY KEY,
  creator_id        TEXT NOT NULL,
  topic_id          TEXT NOT NULL,
  prediction_name   TEXT NOT NULL,
  prediction_date   TEXT NOT NULL DEFAULT (datetime('now')),
  resolution_date   TEXT NOT NULL,
  is_resolved       BOOLEAN DEFAULT FALSE,
  is_correct        BOOLEAN,
  resolved_by       TEXT,
  resolved_at       TEXT,
  FOREIGN KEY (creator_id) REFERENCES Users(user_id),
  FOREIGN KEY (topic_id) REFERENCES Topics(topic_id),
  FOREIGN KEY (resolved_by) REFERENCES Users(user_id)
);

CREATE TABLE PredictionVotes (
  vote_id           TEXT PRIMARY KEY,
  prediction_id     TEXT NOT NULL,
  user_id           TEXT NOT NULL,
  predicted_outcome BOOLEAN NOT NULL,
  voted_at          TEXT NOT NULL DEFAULT (datetime('now')),
  FOREIGN KEY (prediction_id) REFERENCES Predictions(prediction_id),
  FOREIGN KEY (user_id) REFERENCES Users(user_id),
  UNIQUE (prediction_id, user_id)
);
