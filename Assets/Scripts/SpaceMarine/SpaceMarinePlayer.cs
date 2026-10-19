using UnityEngine;
using UnityEngine.InputSystem;

public class SpaceMarinePlayer : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float speed = 10f;
    [SerializeField] private float rotationSpeed = 0.1f;
    [SerializeField] private float jumpHeight = 2f;

    [Header("Inputs")]
    [SerializeField] private InputActionReference moveAction;
    [SerializeField] private InputActionReference jumpAction;
    [SerializeField] private InputActionReference shootBulletAction;
    [SerializeField] private InputActionReference shootMissileAction;

    [Header("Projectiles")]
    [SerializeField] private Transform projectileSpawnPoint;
    private ObjectPool bulletPool;
    private ObjectPool missilePool;

    private Camera mainCamera;
    private CharacterController characterController;
    private float verticalVelocity;

    const int MaxBullets = 50;
    const int MaxMissiles = 10;


    private void Awake()
    {
        mainCamera = Camera.main;
        characterController = GetComponent<CharacterController>();
        bulletPool = Finder.ObjectPools.BulletObjectPools;
        missilePool = Finder.ObjectPools.MissileObjectPool;
    }
    
    private void OnEnable()
    {
        Finder.EventChannels.OnGameEnded += OnGameEnded;
    }

    private void OnDisable()
    {
        var eventChannels = Finder.EventChannels;
        if (eventChannels != null)
            eventChannels.OnGameEnded -= OnGameEnded;
    }

    private void Update()
    {
        var horizontalMove = UpdateHorizontalMovement();
        var verticalMove = UpdateVerticalMovement();
        ShootBulletAction();
        ShootMissiles();

        characterController.Move(horizontalMove + verticalMove);
    }

    private Vector3 UpdateHorizontalMovement()
    {
        // Obtenir différents vecteurs : forward (qui pointe où regarde la caméra) et right (qui pointe à droite d'où
        // regarde la caméra. C'est utile pour savoir dans quelle direction faire avancer le joueur.
        var cameraTransform = mainCamera.transform;
        var forward = cameraTransform.forward;
        var right = cameraTransform.right;

        // Le joueur avance sur l'axe X et Z (à l'horizontal). On a pas besoin du Y (la verticale).
        forward.y = 0;
        right.y = 0;

        // On lit la direction dans lequel le joueur désire aller. On va appeler ça l'entrée.
        var moveInput = moveAction.action.ReadValue<Vector2>();

        // S'il ne désire pas bouger, on DOIT indiquer au CharacterController de ne pas bouger.
        if (moveInput == Vector2.zero)
        {
            return Vector3.zero;
        }
        else
        {
            // L'axe Y de l'entrée indique le mouvement haut/bas. On le multiplie avec le vecteur "forward".
            // L'axe X de l'entrée indique le mouvement gauche/droite. On le multiplie avec le vecteur "right".
            // L'addition des deux produit le mouvement complet. Il ne reste qu'à l'appliquer (comme une translation).
            var moveDirection = forward * moveInput.y + right * moveInput.x;
            var move = moveDirection * (speed * Time.deltaTime);

            // On va tourner le joueur dans le sens où il se déplace. La fonction "LookRotation" nous donne la rotation
            // à appliquer pour qu'un objet pointe dans une direction (ici, la direction du joueur).
            var lookRotation = Quaternion.LookRotation(moveDirection);
            // On ne veut pas nécessairement que ce soit instantané. Cette ligne fera une légère interpolation.
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, rotationSpeed);

            return move;
        }
    }

    private Vector3 UpdateVerticalMovement()
    {
        var up = transform.up;
        var gravity = Physics.gravity.y;

        //get player infos
        var isGrounded = characterController.isGrounded;
        var wantToJump = jumpAction.action.WasPressedThisFrame();

        //si touche le sol, alors velocity = 0.
        if (isGrounded)
        {
            verticalVelocity = 0;
        }

        // Si le joueur veut sauter, ajouter velocié verticale.
        if (isGrounded && wantToJump)
        {
            verticalVelocity = Mathf.Sqrt(2 * -gravity * jumpHeight);
        }

        // Appliquer la gravité
        verticalVelocity += gravity * Time.deltaTime;

        //calculer le mouvement
        return up * (verticalVelocity * Time.deltaTime);
    }

    private void ShootBulletAction()
    {
        if (shootBulletAction.action.triggered)
        {
            if (bulletPool == null) return; // Defensive : évite les appels si le pool est manquant
            if (bulletPool.ActiveCount < MaxBullets)
            {
                bulletPool.Place(projectileSpawnPoint.position, projectileSpawnPoint.rotation);
            }
        }
    }

    private void ShootMissiles()
    {
        if (shootMissileAction.action.triggered)
        {
            var gameController = Finder.GameController;
            if (gameController == null)
            {
                return;
            }

            if (gameController.CanUseMissiles())
            {
                //TO-DO : tirer un projectile en le créant à la position du joueur, avec la rotation du joueur.
                if (missilePool == null) return; // Defensive : évite les appels si le pool est manquant
                if (missilePool.ActiveCount < MaxMissiles)
                {
                    missilePool.Place(projectileSpawnPoint.position, projectileSpawnPoint.rotation);
                    gameController.UseMissiles();
                }  
            }
        }
    }
    
    private void OnGameEnded(bool isVictory)
    {
        if (!isVictory)
            enabled = false;
    }

}