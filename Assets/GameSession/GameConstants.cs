using UnityEngine;
using System.Collections;

public class GameConstants {

	// Gameplay
	public const int PLAYER_COUNT = 2;
	public const int NUM_TRIBES_PER_PLAYER = 4;

	public const float RECRUITMENT_AREA_DEFAULT_SPAWN_TIME = 0.5f;
	public const float RECRUITMENT_AREA_GROUND_WIDTH = 15.0f;
	public const float RECRUITMENT_AREA_GROUND_Y = -0.54f;
	public const float RECRUITMENT_UNIT_WALK_SPEED = 0.5f;
	public const float RECRUITMENT_UNIT_ANIMATION_SPEED = 8.5f;
	public const float RECRUITMENT_UNIT_RUN_SPEED = 2.0f;

	public const float WORKING_AREA_GROUND_WIDTH = 5.0f;
	public const float WORKING_AREA_GROUND_Y = -0.54f;

	public const int PREGAME_DISPLAY_TIME = 110;
	public const int GO_MESSAGE_DISPLAY_TIME = 40;
	
	public const int MAX_NUMBER_OF_ACTIVE_LABORATORIES = 1;

	/* Balancing */
	
	public const int TRIBE_STARTING_UNIT_COUNT = 4;
	public const int TRIBE_UNITS_PER_BEDCHAMBER = 4;

	public const float BASE_TOWER_SEGMENT_ACTION_TIME = 15.0f; // Always takes this full time
	
	public const int CONSTRUCTION_TOWER_SEGMENT_TRIBE_COST = 0;
	
	public const float BEDCHAMBERS_TOWER_SEGMENT_BUILD_TIME = 60.0f;
	
	public const float BALLISTA_TOWER_SEGMENT_BUILD_TIME = 60.0f;
	public const float BALLISTA_TOWER_SEGMENT_ACTION_TIME = 180.0f;
	public const int BALLISTA_TOWER_SEGMENT_TRIBE_COST = 6;
	
	public const float CANNONS_TOWER_SEGMENT_BUILD_TIME = 60.0f;
	public const float CANNONS_TOWER_SEGMENT_ACTION_TIME = 120.0f;
	public const int CANNONS_TOWER_SEGMENT_TRIBE_COST = 9;
	
	public const float LABORATORY_TOWER_SEGMENT_BUILD_TIME = 60.0f;
	public const float LABORATORY_TOWER_SEGMENT_ACTION_TIME = 1.0f;
	public const int LABORATORY_TOWER_SEGMENT_TRIBE_COST = 6;

	public const float WIZARDTOWER_TOWER_SEGMENT_BUILD_TIME = 60.0f;
	public const float WIZARDTOWER_TOWER_SEGMENT_ACTION_TIME = 120.0f;
	public const int WIZARDTOWER_TOWER_SEGMENT_TRIBE_COST = 8;

	public const float MURDERHOLES_TOWER_SEGMENT_BUILD_TIME = 60.0f;
	public const float MURDERHOLES_TOWER_SEGMENT_ACTION_TIME = 60.0f;
	public const int MURDERHOLES_TOWER_SEGMENT_TRIBE_COST = 4;

	public const float WIN_TOWER_SEGMENT_BUILD_TIME = 30.0f; // Always takes this full time
	public const int WIN_TOWER_SEGMENT_TRIBE_SIZE = 20; // Number of workers consumed per tribe when building

}
