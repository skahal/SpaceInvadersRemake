using UnityEngine;
using System.Collections;
using System.Linq;
using Skahal.Tweening;
using Spine.Unity;
using Skahal.SpaceInvadersRemake;

/// <summary>
/// Represents an individual alien.
/// </summary>
public class Alien : ShooterBase
{
    private AliensWave _wave;
    private bool _canShoot;
    private SkeletonAnimation _animation;
    private AudioSource _audioSource;

    [SerializeField]
    AlienKind _kind;

    public AudioClip DieAudio;
    public Vector3 OtherAlienHitShakeAmount = new Vector3(10f, 10, 10f);

    [HideInInspector] public int Row;
    [HideInInspector] public float AnimationSpeed = 1f;
    [HideInInspector] public bool IsAlive = true;

    protected override void Awake()
    {
        base.Awake();
        _animation = GetComponent<SkeletonAnimation>();
        _audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        _wave = gameObject.transform.parent.GetComponent<AliensWave>();
        StartCoroutine(CanShootAgain());
    }

    public void Move(float x, float y)
    {
        transform.position += new Vector3(x, y);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.IsAlienVerticalEdge())
        {
            _wave.Flip();
        }

        if (other.IsProjectile())
        {
            var projectile = other.GetComponent<Projectile>();

            if (projectile.IsTargetingAlien)
            {
                Score.Instance.Sum(gameObject);
                Die();
            }
        }
        else if (other.IsCannonZone())
        {
            Cannon.Instance.Die();
        }
    }

    void OnSpawnBegin()
    {
        Projectile.DestroyIt();
    }

    void OnSpawnEnd()
    {
    }

    protected override bool CanShoot()
    {
        var result = false;

        if (_canShoot && Random.Range(0, 1) <= Game.Instance.AlienShootProbability)
        {
            var hit = Physics2D.LinecastAll(
                transform.position,
                new Vector2(transform.position.x, Cannon.Instance.transform.position.y),
                LayerMask.GetMask("AlienBody"));

            var aliensDownCount = hit.Count();

            result = aliensDownCount == 1;
        }

        if (_canShoot)
        {
            _canShoot = false;
            StartCoroutine(CanShootAgain());
        }

        return result;
    }

    IEnumerator CanShootAgain()
    {
        yield return new WaitForSeconds(Random.Range(0, Game.Instance.AlienShootInterval));
        _canShoot = true;
    }

    protected override void PerformShoot()
    {
        if (_kind.ShootingAnimated)
        {
            _animation.state.SetAnimation(0, "Shooting", false).Complete += (trackEntry) =>
            {
                base.PerformShoot();
                _animation.state.SetAnimation(0, "Idle", true);
            };
        }
        else
            base.PerformShoot();
    }

    public void Die()
    {
        Game.Instance.RaiseMessage("OnAlienHit", gameObject);
        _audioSource.PlayOneShot(DieAudio);
        GetComponent<BoxCollider2D>().enabled = false;
        transform.Find("SkeletonUtility-Root").gameObject.SetActive(false);
        transform.Find("Body").gameObject.SetActive(false);

        GetComponentInChildren<SpritePixel3DExplosion>().Explode();

        CheckAliensAlive();

        StopAllCoroutines();
        GetComponent<MeshRenderer>().enabled = false;
        IsAlive = false;
    }

    void CheckAliensAlive()
    {
        var aliensAlive = _wave.Aliens.Count(a => a.IsAlive) - 1;

        if (aliensAlive == 0)
        {
            Game.Instance.NextLevel();
        }
        else
        {
            SendMessageUpwards("OnAlienDie", aliensAlive);
        }
    }

    void OnAlienHit(GameObject sender)
    {
        if (sender != gameObject)
        {
            iTweenHelper.ShakeRotation(
                    gameObject,
                    iT.ShakeRotation.amount, OtherAlienHitShakeAmount);
        }
    }
}
