using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

public class Misc
{
    private static Color g_color = Color.white;
    private static Vector3 g_vector3 = Vector3.zero;
    private static Vector3 p0 = Vector3.zero;
    private static Vector3 p1 = Vector3.zero;
    private static Vector3 p2 = Vector3.zero;
    private static Vector3 p3 = Vector3.zero;

    #region ETC
    public static float getAngle(float angle)
    {
        while (angle > 180) angle -= 360;
        return angle;
    }

    public static float getAngle360(float angle)
    {
        while (angle < 0)   angle += 360;
        while (angle > 360) angle -= 360;
        return angle;
    }

    public static float easeInOutQuad(float start, float end, float value)
    {
        value /= 0.5f;
        end -= start;
        if (value < 1) return end / 2 * value * value + start;
        value--;
        return -end / 2 * (value * (value - 2) - 1) + start;
    }

    public static float easeInQuad(float start, float end, float value)
    {
        end -= start;
        return end * value * value + start;
    }

    public static float easeOutQuad(float start, float end, float value)
    {
        end -= start;
        return -end * value * (value - 2) + start;
    }

    public static float Liner(float x0, float x1, float x, float y0, float y1)
    {
        if (x > x1) { return y1; }
        if (x < x0) { return y0; }
        float a = (y1 - y0) / (x1 - x0);
        float b = (-a * x0) + y0;
        return (a * x) + b;
    }

    public static float Liner(float x0, float x1, float x2, float x, float y0, float y1, float y2)
    {
        if (x > x2) { return y2; }
        if (x < x0) { return y0; }
        float a = ((y2 - y0) * (x1 - x0) - (y1 - y0) * (x2 - x0)) / (((x2 * x2) - (x0 * x0)) * (x1 - x0) - ((x1 * x1) - (x0 * x0)) * (x2 - x0));
        float b = ((y1 - y0) * ((x2 * x2) - (x0 * x0)) - (y2 - y0) * ((x1 * x1) - (x0 * x0))) / ((x1 - x0) * ((x2 * x2) - (x0 * x0)) - (x2 - x0) * ((x1 * x1) - (x0 * x0)));
        float c = ((-a * x0) * x0) - (b * x0) + y0;
        return ((a * x) * x) + (b * x) + c;
    }

    public static Color Liner(float x0, float x1, float x2, float x, Color y0, Color y1, Color y2)
    {
        g_color.r = Liner(x0, x1, x2, x, y0.r, y1.r, y2.r);
        g_color.g = Liner(x0, x1, x2, x, y0.g, y1.g, y2.g);
        g_color.b = Liner(x0, x1, x2, x, y0.b, y1.b, y2.b);
        g_color.a = Liner(x0, x1, x2, x, y0.a, y1.a, y2.a);
        return g_color;
    }

    private static float linear(float start, float end, float value)
    {
        return Mathf.Lerp(start, end, value);
    }

    private static float clerp(float start, float end, float value)
    {
        float min = 0.0f;
        float max = 360.0f;
        float half = Mathf.Abs((max - min) / 2.0f);
        float retval = 0.0f;
        float diff = 0.0f;
        if ((end - start) < -half)
        {
            diff = ((max - start) + end) * value;
            retval = start + diff;
        }
        else if ((end - start) > half)
        {
            diff = -((max - end) + start) * value;
            retval = start + diff;
        }
        else retval = start + (end - start) * value;
        return retval;
    }

    private static float spring(float start, float end, float value)
    {
        value = Mathf.Clamp01(value);
        value = (Mathf.Sin(value * Mathf.PI * (0.2f + 2.5f * value * value * value)) * Mathf.Pow(1f - value, 2.2f) + value) * (1f + (1.2f * (1f - value)));
        return start + (end - start) * value;
    }

    private static float punch(float amplitude, float value)
    {
        float s = 9;
        if (value == 0)
        {
            return 0;
        }
        if (value == 1)
        {
            return 0;
        }
        float period = 1 * 0.3f;
        s = period / (2 * Mathf.PI) * Mathf.Asin(0);
        return (amplitude * Mathf.Pow(2, -10 * value) * Mathf.Sin((value * 1 - s) * (2 * Mathf.PI) / period));
    }

    /* GFX47 MOD START */
    private static float easeInBounce(float start, float end, float value)
    {
        end -= start;
        float d = 1f;
        return end - easeOutBounce(0, end, d - value) + start;
    }
    /* GFX47 MOD END */

    /* GFX47 MOD START */
    //private float bounce(float start, float end, float value){
    private static float easeOutBounce(float start, float end, float value)
    {
        value /= 1f;
        end -= start;
        if (value < (1 / 2.75f))
        {
            return end * (7.5625f * value * value) + start;
        }
        else if (value < (2 / 2.75f))
        {
            value -= (1.5f / 2.75f);
            return end * (7.5625f * (value) * value + .75f) + start;
        }
        else if (value < (2.5 / 2.75))
        {
            value -= (2.25f / 2.75f);
            return end * (7.5625f * (value) * value + .9375f) + start;
        }
        else
        {
            value -= (2.625f / 2.75f);
            return end * (7.5625f * (value) * value + .984375f) + start;
        }
    }
    /* GFX47 MOD END */

    /* GFX47 MOD START */
    private static float easeInOutBounce(float start, float end, float value)
    {
        end -= start;
        float d = 1f;
        if (value < d / 2) return easeInBounce(0, end, value * 2) * 0.5f + start;
        else return easeOutBounce(0, end, value * 2 - d) * 0.5f + end * 0.5f + start;
    }
    /* GFX47 MOD END */

    private static float easeInBack(float start, float end, float value)
    {
        end -= start;
        value /= 1;
        float s = 1.70158f;
        return end * (value) * value * ((s + 1) * value - s) + start;
    }

    private static float easeOutBack(float start, float end, float value)
    {
        float s = 1.70158f;
        end -= start;
        value = (value / 1) - 1;
        return end * ((value) * value * ((s + 1) * value + s) + 1) + start;
    }

    private static float easeInOutBack(float start, float end, float value)
    {
        float s = 1.70158f;
        end -= start;
        value /= .5f;
        if ((value) < 1)
        {
            s *= (1.525f);
            return end / 2 * (value * value * (((s) + 1) * value - s)) + start;
        }
        value -= 2;
        s *= (1.525f);
        return end / 2 * ((value) * value * (((s) + 1) * value + s) + 2) + start;
    }

    public static float Shake(float t, float freq = 10f, bool loop = false)
    {
        if (loop)
            return Mathf.Sin(t * freq);
        else
            return Mathf.Sin(t * freq) * Mathf.Pow(0.5f, t);
    }

    public static float MPStoKMPH(float f)
    {
        return f * 3.6f;
    }

    public static float KMPHtoMPS(float f)
    {
        return f / 3.6f;
    }
#endregion

    #region HERMITE_CURVES

    public static Vector3 HermiteCurves(Vector3 p0, Vector3 p1, Vector3 m0, Vector3 m1, float t)
    {
        return (2 * t * t * t - 3 * t * t + 1) * p0 + (-2 * t * t * t + 3 * t * t) * p1 + (t * t * t - 2 * t * t + t) * m0 + (t * t * t - t * t) * m1;
    }

    //public static Vector3 HermitePosition(Vector3[] path, float t)
    //{
    //    int numSections = path.Length - 1;
    //    int currPt = Mathf.Min(Mathf.FloorToInt(t * (float)numSections), numSections);
    //    float u = t * (float)numSections - (float)currPt;

    //    p0 = path[currPt];
    //    p1 = path[currPt + 1];

    //    if (currPt > 0)
    //    {
    //        p2 = 0.5f * (path[currPt + 1] - path[currPt - 1]);
    //    }
    //    else
    //    {
    //        p2 = path[currPt + 1] - path[currPt];
    //    }

    //    if (currPt < path.Length - 2)
    //    {
    //        p3 = 0.5f * (path[currPt + 2] - path[currPt]);
    //    }
    //    else
    //    {
    //        p3 = path[currPt + 1] - path[currPt];
    //    }

    //    return BobsleighUtility.HermiteCurves(p0, p1, p2, p3, u);
    //}

    public static Vector3 HermiteDirectioin(Vector3[] path, float t)
    {
        int numSections = path.Length - 1;
        int currPt = Mathf.Min(Mathf.FloorToInt(t * (float)numSections), numSections);
        float u = t * (float)numSections - (float)currPt;

        p0 = path[currPt];
        p1 = path[currPt + 1];

        if (currPt > 0)
        {
            p2 = 0.5f * (path[currPt + 1] - path[currPt - 1]);
        }
        else
        {
            p2 = path[currPt + 1] - path[currPt];
        }

        if (currPt < path.Length - 2)
        {
            p3 = 0.5f * (path[currPt + 2] - path[currPt]);
        }
        else
        {
            p3 = path[currPt + 1] - path[currPt];
        }
        return Vector3.Lerp(p2, p3, u);
    }

#endregion
    
    #region CATMULL_ROM
        
    public class CatmullRom
    {
        private List<Vector3> m_points = new List<Vector3>();
        private float m_percent = 0f;
        private float m_overPercent = 0f;
        private bool m_isPlay;

        public float Delay { get; set; }

        public float IsPercent
        { get { return m_percent; } }

        public float OverPercent
        { get { return m_overPercent; } }

        public bool IsPlay
        { get { return m_isPlay; } }
        
        public void Play(float overPercent, float delay)
        {
            if (m_points.Count < 2)
                return;

            m_isPlay = true;
            m_percent = overPercent;
            Delay = delay;
        }

        public void Stop()
        {
            m_isPlay = false;
            m_percent = 0f;
        }

        public Vector3 Update(float deltaTime)
        {
            m_percent += deltaTime * Delay;
            if (m_percent >= 1f)
            {
                m_overPercent = m_percent - 1f;
                m_isPlay = false;
            }
            return CurrentPosition(m_percent);
        }

        public void AddPoint(Vector3 point)
        {
            m_points.Add(point);
        }

        public void AddPoints(Vector3[] points)
        {
            for (int i = 0; i < points.Length; ++i)
            {
                m_points.Add(points[i]);
            }
        }

        public void SetState(CatmullRom catmull)
        {
            m_percent = catmull.IsPercent;
            m_overPercent = catmull.m_overPercent;
            m_isPlay = catmull.m_isPlay;
            Delay = catmull.Delay;
        }
        public void Clear()
        {
            m_points.Clear();
        }

        public int Length()
        {
            return m_points.Count;
        }

        public Vector3 CurrentPosition(float persent)
        {
            return iTween.PointOnPath(m_points.ToArray(), persent);
        }

        public void OnDrawGizmos(Color color)
        {
            iTween.DrawPathGizmos(m_points.ToArray(), color);
        }
    }

#endregion

    #region OBJECT_TARGET_SENDER

    public class ObjectSender
    {
        public enum EaseType
        {
            None,
            EaseIn,
            EaseOut,
            EaseInOut,
        }

        protected float timer = 0f;

        public Vector3 beginPosition { get; set; }
        public Vector3 beginDirection { get; set; }
        public Vector3 endPosition { get; set; }
        public Vector3 endDirection { get; set; }
        public float life { get; set; }
        public float speed { get; set; }
        public float length { get; set; }
        public float timeStep { get; set; }
        public bool isPlay { get; set; }

        protected delegate float EaseFunc(float p0, float p1, float p2);
        protected EaseFunc function = null;
        
        public ObjectSender()
        {
            isPlay = false;
            speed = 1f;
            timer = 0f;
            life = 0f;
            beginPosition = Vector3.zero;
            beginDirection = Vector3.zero;
            endPosition = Vector3.zero;
            endDirection = Vector3.zero;
        }

        public virtual void Play(Vector3 _begin, Vector3 _end, float _speed)
        {
            speed = _speed;
            beginPosition = _begin;
            beginDirection = _end - _begin;
            endPosition = _end;
            endDirection = beginDirection;
            length = beginDirection.magnitude;
            
            isPlay = true;
            timer = 0f;
            life = 1f;
            SetEaseType();
        }

        public void SetEaseType(EaseType type = EaseType.None)
        {
            switch (type)
            {
                case EaseType.EaseIn:
                    function = easeInQuad;
                    break;
                case EaseType.EaseOut:
                    function = easeOutQuad;
                    break;
                case EaseType.EaseInOut:
                    function = easeInOutQuad;
                    break;
                default:
                    function = Mathf.Lerp;
                    break;
            }
        }

        public virtual Vector3 Update(float deltaTime)
        {
            timeStep = deltaTime * speed;
            timer += timeStep;
            if (timer >= life)
            {
                timer = life;
                isPlay = false;
            }
            return GetPosition(timer / life);
        }

        public Vector3 GetPosition(float t)
        {
            return HermiteCurves(beginPosition, endPosition, beginDirection, endDirection, function(0f, 1f, t));
        }

        public Vector3 GetRotation(float t, float step)
        {
            if (t == 0f)
                return beginDirection.normalized;

            if (t >= 1f)
                return endDirection.normalized;

            Vector3 prevPosition = HermiteCurves(beginPosition, endPosition, beginDirection, endDirection, function(0f, 1f, t - step));
            Vector3 currPosition = HermiteCurves(beginPosition, endPosition, beginDirection, endDirection, function(0f, 1f, t));
            return (currPosition - prevPosition).normalized;
        }

        public virtual void OnDrawGizmos(Vector3 pivot, int count, Color color, bool drawRay = false)
        {
            Vector3 prevPoint = Vector3.zero;
            Vector3 currPoint = Vector3.zero;

            for (int i = 0; i < count; ++i)
            {
                Gizmos.color = color;
                currPoint = pivot + HermiteCurves(beginPosition, endPosition, beginDirection, endDirection, (float)i / (float)count);                

                if (i > 0)
                    Gizmos.DrawLine(prevPoint, currPoint);

                prevPoint = currPoint;
            }

            if (drawRay)
            {
                Gizmos.color = Color.yellow;
                Gizmos.DrawRay(pivot + beginPosition, beginDirection);
                Gizmos.DrawRay(pivot + endPosition, endDirection);
            }
        }
    }

    public class ObjectSender3DTo2D : ObjectSender
    {
        private float m_distance;

        public ObjectSender3DTo2D()
        {
            isPlay = false;
            speed = 1f;
            timer = 0f;
            life = 0f;
        }

        public void Play(Vector3 _begin, Vector3 _end, float _speed, float _distance)
        {
            speed = _speed;
            m_distance = _distance;
            beginPosition = _begin;
            beginDirection = _end - _begin;
            endPosition = _end;
            endDirection = beginDirection;
            isPlay = true;
            timer = 0f;
            SetEaseType();
        }

        public override Vector3 Update(float deltaTime)
        {
            timer += deltaTime * speed;
            if (timer >= life)
            {
                timer = life;
                isPlay = false;
            }
            Ray ray = Camera.main.ScreenPointToRay(endPosition);
            return HermiteCurves(beginPosition, ray.origin + ray.direction * m_distance, beginDirection, endDirection, function(0f, 1f, timer / life));
        }

        public void OnDrawGizmos(int count, Color color)
        {
            Gizmos.color = color;
            Vector3 prevPoint = Vector3.zero;
            Vector3 currPoint = Vector3.zero;

            for (int i = 0; i < count; ++i)
            {
                Ray ray = Camera.main.ScreenPointToRay(endPosition);
                currPoint = HermiteCurves(beginPosition, ray.origin + ray.direction * m_distance, beginDirection, endDirection, (float)i / count);

                if (i > 0)
                    Gizmos.DrawLine(prevPoint, currPoint);

                prevPoint = currPoint;
            }
        }
    }
#endregion    
    
    #region JIGGLE_FORCE

    public struct JiggleForce
    {
        public float targetPos;
        public float dynamicPos;

        public float axis;
        public float stiffness;
        public float mass;
        public float damping;
        public float gravity;

        public float force;
        public float acc;
        public float vel;

        public bool squashAndStretch;
        public float sideStretch;
        public float frontStretch;
        public float stretch;

        public void Initialize()
        {
            axis = 1f;
            stiffness = 10f;
            mass = 1f;
            damping = 0.75f;
            gravity = 0f;
            force = acc = vel = 0f;
            targetPos = 0f;
            dynamicPos = 0f;

            squashAndStretch = true;
            stretch = 0f;
            sideStretch = 0.15f;
            frontStretch = 0.2f;
        }

        public void Update(float deltaTime)
        {
            force = (targetPos - dynamicPos) * stiffness * deltaTime;
            acc = force / mass;
            vel += acc * (1f - damping);
            dynamicPos += vel + force;

            if (squashAndStretch)
            {
                float dynamicValue = dynamicPos - targetPos;
                float stretchMag = Mathf.Abs(dynamicValue);

                if (dynamicValue == 0f) stretch = 1f + (-stretchMag * sideStretch);
                else stretch = 1f + (stretchMag * frontStretch);
            }
        }
    }
#endregion
    
    #region INVINCIBLE_EFFECT
    public class Invincible
    {
        public enum Type
        {
            Object,
            Material,
        }
        protected Type m_type;
        protected GameObject m_object;
        protected Material m_material;
        protected float m_timer;
        protected float m_alpha;
        protected float m_delay;

        public bool IsPlay { get; set; }

        public Invincible()
        {
            m_alpha = 0f;
            m_timer = 0f;
        }

        public virtual void Play(float lifeTime, float delay, Type type)
        {
            IsPlay = true;
            m_type = type;
            m_timer = lifeTime;
            m_delay = delay;
            m_alpha = 0f;
            if (m_material != null)
                m_material.SetColor("_Color", new Color(1f, 1f, 1f, Mathf.Clamp01(m_alpha)));
        }

        public virtual void Stop()
        {
            if (m_object != null)
                m_object.SetActive(true);
            if (m_material != null)
                m_material.SetColor("_Color", Color.white);
        }

        public virtual void SetObject(GameObject _object)
        {
            m_object = _object;
        }

        public virtual void SetMaterial(Material mtrl)
        {
            m_material = mtrl;
        }

        public virtual void Update(float deltaTime)
        {
            if (!IsPlay)
                return;

            m_timer -= deltaTime;
            m_alpha += deltaTime * Mathf.PI * 2f;
            if (m_alpha >= Mathf.PI * 2f)
                m_alpha -= Mathf.PI * 2f;

            if (m_timer <= 0f)
            {
                m_timer = 0f;
                IsPlay = false;
                switch (m_type)
                {
                    case Type.Object:
                        if (m_object != null)
                            m_object.SetActive(true);
                        break;
                    case Type.Material:
                        if (m_material != null)
                            m_material.SetColor("_Color", Color.white);
                        break;
                }
                return;
            }
            switch (m_type)
            {
                case Type.Object:
                    if (m_object != null)
                        m_object.SetActive((Mathf.Sign(Mathf.Sin(m_alpha * m_delay)) > 0) ? true : false);
                    break;
                case Type.Material:
                    if (m_material != null)
                        m_material.SetColor("_Color", new Color(1f, 1f, 1f, Mathf.Abs(Mathf.Sin(m_alpha * m_delay))));
                    break;
            }
        }
    }

    //public class InvincibleAdd : Invincible
    //{        
    //    public override void Update(float deltaTime, float targetValue)
    //    {
    //        if (m_material == null)
    //            return;

    //        m_alpha = Mathf.MoveTowards(m_alpha, targetValue, deltaTime);
    //        if (m_prevAlpha == m_alpha)
    //            return;

    //        if (m_alpha < 1f)
    //        {
    //            m_material.SetColor("_Color", new Color(1f, 1f, 1f, Mathf.Abs(Mathf.Sin(m_alpha * 30f))));
    //        }
    //        else
    //        {
    //            m_material.SetColor("_Color", new Color(1f, 1f, 1f, Mathf.Clamp01(m_alpha)));
    //        }
    //        m_prevAlpha = m_alpha;
    //    }
    //}
#endregion

    #region SHAKE
    public struct Shaking
    {
        public float time;
        public float delta;
        public float freq;
        public float delay;
        public float prevStrength;
        public float currStrength;

        public bool isPlay;
        public bool isLoop;

        public void Initialize()
        {
            isPlay = false;
            isLoop = false;
            time = 0f;
            freq = 10f;
            delta = delay = freq = 0f;
            prevStrength = currStrength = 0f;
        }

        public bool Update(float deltaTime)
        {
            if (!isPlay)
                return false;

            time += deltaTime * delay;
            currStrength = Shake(time, freq, isLoop);
            delta = currStrength - prevStrength;
            prevStrength = currStrength;

            if (!isLoop && time >= freq)
                Stop();

            if (isLoop && time >= Mathf.PI * 2f)
                time -= Mathf.PI * 2f;

            return true;
        }

        public void Play(float _freq, float _duration, bool _loop)
        {
            isPlay = true;
            isLoop = _loop;
            freq = _freq;
            delay = freq / _duration;
            time = 0f;
            delta = 0f;
            prevStrength = currStrength = 0f;
        }

        public void Stop()
        {
            isPlay = false;
        }
    }
#endregion

    #region DRAW_FRUSTUM
    public static void DrawGizmosFrustum(Camera camera)
    {
        Matrix4x4 matrix = Gizmos.matrix;
        Gizmos.matrix = Matrix4x4.TRS(camera.transform.position, camera.transform.rotation, Vector3.one);
        if (camera.orthographic)
        {
            float spread = camera.farClipPlane - camera.nearClipPlane;
            float center = (camera.farClipPlane + camera.nearClipPlane) * 0.5f;
            Gizmos.DrawWireCube(new Vector3(0, 0, center), new Vector3(camera.orthographicSize * 2 * camera.aspect, camera.orthographicSize * 2, spread));
        }
        else
        {
            Gizmos.DrawFrustum(Vector3.zero, camera.fieldOfView, camera.farClipPlane, camera.nearClipPlane, camera.aspect);
        }
        Gizmos.matrix = matrix;
    }

    public static Vector2 GetMainGameViewSize()
    {
        System.Type T = System.Type.GetType("UnityEditor.GameView,UnityEditor");
        System.Reflection.MethodInfo GetSizeOfMainGameView = T.GetMethod("GetSizeOfMainGameView",System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static);
        System.Object Res = GetSizeOfMainGameView.Invoke(null,null);
        return (Vector2)Res;
    }

    public static void DrawGizmosFrustum2(Camera camera)
    {
        // Top left
        Vector3 tlN = camera.ScreenToWorldPoint(new Vector3(Screen.width * 0, Screen.height, camera.nearClipPlane));
        Vector3 tlF = camera.ScreenToWorldPoint(new Vector3(Screen.width * 0, Screen.height, camera.farClipPlane));
 
        // Top right
        Vector3 trN = camera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, camera.nearClipPlane));
        Vector3 trF = camera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, camera.farClipPlane));
 
        // Bottom left
        Vector3 blN = camera.ScreenToWorldPoint(new Vector3(0.0f, 0.0f, camera.nearClipPlane));
        Vector3 blF = camera.ScreenToWorldPoint(new Vector3(0.0f, 0.0f, camera.farClipPlane));
        
        // Bottom right
        Vector3 brN = camera.ScreenToWorldPoint(new Vector3(Screen.width, 0.0f, camera.nearClipPlane));
        Vector3 brF = camera.ScreenToWorldPoint(new Vector3(Screen.width, 0.0f, camera.farClipPlane));
 
        // Scale for aspect ratio
        Vector2 gameViewsize = GetMainGameViewSize();
        float gameViewAspect = gameViewsize.x / gameViewsize.y;
        float s = gameViewAspect / camera.aspect; 
        tlN.x *= s;
        tlF.x *= s;
        trN.x *= s;
        trF.x *= s;
        blN.x *= s;
        blF.x *= s;
        brN.x *= s;
        brF.x *= s;
 
        Gizmos.color = Color.white;
 
        // Near
        Gizmos.DrawLine(tlN, trN);
        Gizmos.DrawLine(trN, brN);
        Gizmos.DrawLine(brN, blN);
        Gizmos.DrawLine(blN, tlN);
        // Far
        Gizmos.DrawLine(tlF, trF);
        Gizmos.DrawLine(trF, brF);
        Gizmos.DrawLine(brF, blF);
        Gizmos.DrawLine(blF, tlF);
        // Sides
        Gizmos.DrawLine(tlN, tlF); 
        Gizmos.DrawLine(trN, trF);
        Gizmos.DrawLine(brN, brF);
        Gizmos.DrawLine(blN, blF);
    }

    public static void DrawGizmosFrustum3(Camera camera, float cubeSize)
    {
        Vector3[] points = new Vector3[8];
        Color gizmosColor = Gizmos.color;
        Gizmos.color = Color.red;
        points[0] = camera.ViewportToWorldPoint(new Vector3(0, 0, camera.farClipPlane));
        points[1] = camera.ViewportToWorldPoint(new Vector3(0, 1, camera.farClipPlane));
        points[2] = camera.ViewportToWorldPoint(new Vector3(1, 1, camera.farClipPlane));
        points[3] = camera.ViewportToWorldPoint(new Vector3(1, 0, camera.farClipPlane));
        points[4] = camera.ViewportToWorldPoint(new Vector3(0, 0, camera.nearClipPlane)); // bottom_left
        points[5] = camera.ViewportToWorldPoint(new Vector3(0, 1, camera.nearClipPlane)); // top_left
        points[6] = camera.ViewportToWorldPoint(new Vector3(1, 1, camera.nearClipPlane)); // top_right
        points[7] = camera.ViewportToWorldPoint(new Vector3(1, 0, camera.nearClipPlane)); // bottom_right

        for (int i = 0; i < points.Length; ++i)
        {
            Gizmos.DrawCube(points[i], Vector3.one * cubeSize);
        }
        Gizmos.color = gizmosColor;
    }
#endregion
}