SELECT users.Username, knownips.IP, users.UUID, users.MinutesIngame, (
		SELECT COUNT(*) FROM bans WHERE Username = users.Username
			AND (bans.Expiry IS NULL OR UNIX_TIMESTAMP() < bans.Expiry) # The ban hasn't expired yet
            AND bans.UnbanDate IS NULL # The ban hasn't been manually revoked
    ) as NumActiveBans FROM users
	LEFT JOIN knownips ON users.Username = knownips.Username;