# Build stage
FROM node:18-alpine AS build

WORKDIR /app

# Copy package.json and package-lock.json
COPY package*.json ./

# Install dependencies
RUN npm ci

# Copy the rest of the code
COPY . .

# Build the app for production
RUN npm run build

# Production stage
FROM node:18-alpine AS production

# Install serve to run the application
RUN npm install -g serve

WORKDIR /app

# Copy built app from the build stage
COPY --from=build /app/build ./build

# Set the command to start the app
CMD ["serve", "-s", "build", "-l", "3000"]

# Expose port 3000
EXPOSE 3000
