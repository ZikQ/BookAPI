namespace BookAPI;

public class NotFoundException(string message) : Exception(message);

public class ConflictException(string message) : Exception(message);

public class UnauthorizedException(string message) : Exception(message);

public class AlreadyRegisteredException(string message) : Exception(message);