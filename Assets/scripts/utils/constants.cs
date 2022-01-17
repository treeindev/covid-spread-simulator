public static class Constants
{
  // Map coordinates
  public static float MAP_Z = 0.3f;
  public static float[] MAP_TOP_LEFT = new float[] {-13.4f, 10.0f};
  public static float[] MAP_TOP_RIGHT = new float[]{13.4f, 10.0f};
  public static float[] MAP_BOTTOM_LEFT = new float[]{-13.4f, -10.0f};
  public static float[] MAP_BOTTOM_RIGHT = new float[]{13.4f, -10.0f};

  // Restaurant coordinates
  public static float[] RESTAURANT_TOP_LEFT = new float[]{-13.4f, 10.0f};
  public static float[] RESTAURANT_TOP_RIGHT = new float[]{0.0f, 10.0f};
  public static float[] RESTAURANT_BOTTOM_LEFT = new float[]{-13.4f, 0.0f};
  public static float[] RESTAURANT_BOTTOM_RIGHT = new float[]{0.0f, 0.0f};

  // Home coordinates
  public static float[] HOME_TOP_LEFT = new float[]{-13.4f, 0.0f};
  public static float[] HOME_TOP_RIGHT = new float[]{0.0f, 0.0f};
  public static float[] HOME_BOTTOM_LEFT = new float[]{-13.4f, -10.0f};
  public static float[] HOME_BOTTOM_RIGHT = new float[]{0.0f, -10.0f};

  // Park coordinates
  public static float[] PARK_TOP_LEFT = new float[]{0.0f, 0.0f};
  public static float[] PARK_TOP_RIGHT = new float[]{13.4f, 0};
  public static float[] PARK_BOTTOM_LEFT = new float[]{0.0f, -10.0f};
  public static float[] PARK_BOTTOM_RIGHT = new float[]{13.4f, -10.0f};

  // Hospital coordinates
  public static float[] HOSPITAL_TOP_LEFT = new float[]{0.0f, 10.0f};
  public static float[] HOSPITAL_TOP_RIGHT = new float[]{13.4f, 10};
  public static float[] HOSPITAL_BOTTOM_LEFT = new float[]{0.0f, 0.0f};
  public static float[] HOSPITAL_BOTTOM_RIGHT = new float[]{13.4f, 0.0f};

  // Materials
  public static string MATERIAL_INFECTED = "infected";
  public static string MATERIAL_NORMAL = "normal";
  public static string MATERIAL_PROTECTED_SEMI = "material_protected_semi";
  public static string MATERIAL_PROTECTED_FULL = "material_protected_full";

  // GameObject details
  public static float SUBJECT_COLLISION_RADIUS = 0.3f;
  public static float SUBJECT_SCALE_X = 0.15f;
  public static float SUBJECT_SCALE_Y = 0.15f;
  public static float SUBJECT_SCALE_Z = 0.15f;

  // COVID Related Data
  // 40% of COVID infected are found to be asymptomatic
  // https://jamanetwork.com/journals/jamanetworkopen/fullarticle/2787098
  public static int SUBJECT_ASYMPTOMATIC_LEVEL = 95;
  // This is the percentage of vaccined people in a certain area.
  public static int SUBJECT_VACCINED_PERCENTAGE = 50;
  // Percentage of probability of infection when contact with positive based on protection
  public static int SUBJECT_CONTACT_BOTH_PROTECTED = 0;
  public static int SUBJECT_CONTACT_ONE_PROTECTED = 30;
  public static int SUBJECT_CONTACT_NONE_PROTECTED = 75;

}